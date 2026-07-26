# ============================================================
#  SERVIDOR MCP PARA BLENDER - VERSIÓN 3.0
#  Herramientas: modificadores, animación básica y avanzada,
#  iluminación, cámara, rigging, NLA, drivers, historial
# ============================================================

import socket
import json
import asyncio
import os
from datetime import datetime
from mcp.server import Server
from mcp.server.stdio import stdio_server
from mcp import types


# ============================================================
#  CONFIGURACIÓN
# ============================================================
BLENDER_HOST = "127.0.0.1"
BLENDER_PORT = 9876

# Carpeta donde se guarda el historial (junto al server.py)
HISTORY_DIR = os.path.join(os.path.dirname(os.path.abspath(__file__)), "historial")
os.makedirs(HISTORY_DIR, exist_ok=True)

HISTORY_FILE = os.path.join(HISTORY_DIR, "historial.json")


# ============================================================
#  SISTEMA DE HISTORIAL
#  Guarda comandos, estados de escena y errores en JSON
# ============================================================

def load_history() -> list:
    """Carga el historial existente desde el archivo JSON."""
    if os.path.exists(HISTORY_FILE):
        try:
            with open(HISTORY_FILE, "r", encoding="utf-8") as f:
                return json.load(f)
        except:
            return []
    return []

def save_history(history: list):
    """Guarda el historial en el archivo JSON."""
    try:
        with open(HISTORY_FILE, "w", encoding="utf-8") as f:
            json.dump(history, f, indent=2, ensure_ascii=False)
    except Exception as e:
        print(f"[MCP] Error guardando historial: {e}")

def log_event(event_type: str, data: dict):
    """
    Agrega un evento al historial.
    event_type puede ser: "comando", "escena", "error"
    """
    history = load_history()
    entry = {
        "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        "tipo": event_type,
        **data
    }
    history.append(entry)
    # Mantenemos solo los últimos 500 eventos para no crecer infinito
    if len(history) > 500:
        history = history[-500:]
    save_history(history)


# ============================================================
#  FUNCIÓN PARA ENVIAR COMANDOS A BLENDER
# ============================================================
def send_to_blender(command: dict) -> dict:
    """Envía un comando JSON al addon de Blender y retorna la respuesta."""
    try:
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        sock.settimeout(30)
        sock.connect((BLENDER_HOST, BLENDER_PORT))

        message = json.dumps(command) + "\n"
        sock.sendall(message.encode("utf-8"))

        response_data = b""
        while True:
            chunk = sock.recv(65536)
            if not chunk:
                break
            response_data += chunk
            if response_data.endswith(b"\n"):
                break

        sock.close()
        return json.loads(response_data.decode("utf-8").strip())

    except ConnectionRefusedError:
        return {"error": "No se pudo conectar a Blender. Asegúrate de que esté abierto y el addon activo."}
    except socket.timeout:
        return {"error": "Blender tardó demasiado en responder (timeout 30s)"}
    except Exception as e:
        return {"error": f"Error de conexión: {str(e)}"}


# ============================================================
#  SERVIDOR MCP
# ============================================================
server = Server("blender-mcp")


@server.list_tools()
async def list_tools() -> list[types.Tool]:
    return [

        # ── HERRAMIENTAS ORIGINALES ──────────────────────────────

        types.Tool(
            name="get_scene_info",
            description="Obtiene información general de la escena actual de Blender: nombre, frame, motor de render, resolución, cantidad de objetos.",
            inputSchema={"type": "object", "properties": {}, "required": []},
        ),
        types.Tool(
            name="get_objects",
            description="Lista todos los objetos en la escena con nombre, tipo, posición, rotación, escala y si están seleccionados.",
            inputSchema={"type": "object", "properties": {}, "required": []},
        ),
        types.Tool(
            name="get_active_object",
            description="Obtiene información detallada del objeto actualmente seleccionado, incluyendo materiales.",
            inputSchema={"type": "object", "properties": {}, "required": []},
        ),
        types.Tool(
            name="execute_code",
            description="Ejecuta código Python directamente en Blender usando bpy. Para retornar un valor asigna a '__result__'.",
            inputSchema={
                "type": "object",
                "properties": {
                    "code": {"type": "string", "description": "Código Python válido para ejecutar en Blender."}
                },
                "required": ["code"],
            },
        ),
        types.Tool(
            name="create_object",
            description="Crea un objeto 3D básico. Tipos: CUBE, SPHERE, CYLINDER, PLANE, CONE, TORUS, MONKEY.",
            inputSchema={
                "type": "object",
                "properties": {
                    "object_type": {"type": "string", "enum": ["CUBE","SPHERE","CYLINDER","PLANE","CONE","TORUS","MONKEY"]},
                    "name": {"type": "string"},
                    "location": {"type": "array", "items": {"type": "number"}},
                },
                "required": ["object_type"],
            },
        ),
        types.Tool(
            name="set_material",
            description="Aplica un material con color [R,G,B] (valores 0.0-1.0) a un objeto.",
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string"},
                    "color": {"type": "array", "items": {"type": "number"}, "description": "Color [R,G,B] de 0.0 a 1.0"},
                    "material_name": {"type": "string"},
                },
                "required": ["object_name", "color"],
            },
        ),
        types.Tool(
            name="delete_object",
            description="Elimina un objeto de la escena por su nombre.",
            inputSchema={
                "type": "object",
                "properties": {"object_name": {"type": "string"}},
                "required": ["object_name"],
            },
        ),
        types.Tool(
            name="move_object",
            description="Mueve un objeto a una nueva posición [x, y, z].",
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string"},
                    "location": {"type": "array", "items": {"type": "number"}},
                },
                "required": ["object_name", "location"],
            },
        ),
        types.Tool(
            name="render_scene",
            description="Renderiza la escena actual. Puede especificarse ruta de salida.",
            inputSchema={
                "type": "object",
                "properties": {"output_path": {"type": "string"}},
                "required": [],
            },
        ),

        # ── MODIFICADORES ────────────────────────────────────────

        types.Tool(
            name="add_modifier",
            description=(
                "Agrega un modificador a un objeto. "
                "Modificadores disponibles: SUBSURF (suaviza la malla), BEVEL (bisela bordes), "
                "SOLIDIFY (da grosor a superficies), MIRROR (espeja el objeto), "
                "ARRAY (duplica en array), BOOLEAN (operaciones booleanas), "
                "DECIMATE (reduce polígonos), WIREFRAME (convierte a estructura de alambre)."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string", "description": "Nombre del objeto"},
                    "modifier_type": {
                        "type": "string",
                        "enum": ["SUBSURF","BEVEL","SOLIDIFY","MIRROR","ARRAY","BOOLEAN","DECIMATE","WIREFRAME"],
                        "description": "Tipo de modificador"
                    },
                    "settings": {
                        "type": "object",
                        "description": (
                            "Configuración opcional del modificador. Ejemplos: "
                            "SUBSURF: {levels: 2}, "
                            "BEVEL: {width: 0.1, segments: 3}, "
                            "SOLIDIFY: {thickness: 0.05}, "
                            "ARRAY: {count: 3, offset_x: 2.0}, "
                            "DECIMATE: {ratio: 0.5}"
                        )
                    },
                },
                "required": ["object_name", "modifier_type"],
            },
        ),
        types.Tool(
            name="apply_modifier",
            description="Aplica (hace permanente) un modificador en un objeto.",
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string"},
                    "modifier_name": {"type": "string", "description": "Nombre del modificador tal como aparece en Blender"},
                },
                "required": ["object_name", "modifier_name"],
            },
        ),

        # ── ANIMACIÓN ────────────────────────────────────────────

        types.Tool(
            name="set_keyframe",
            description=(
                "Inserta un keyframe en un objeto para animación. "
                "Puedes animar: location (posición), rotation (rotación), scale (escala), o all (todo)."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string"},
                    "frame": {"type": "integer", "description": "Número de frame donde insertar el keyframe"},
                    "property": {
                        "type": "string",
                        "enum": ["location","rotation","scale","all"],
                        "description": "Propiedad a animar"
                    },
                    "values": {
                        "type": "array",
                        "items": {"type": "number"},
                        "description": "Valores [x,y,z] para la propiedad (opcional, usa los valores actuales si no se especifica)"
                    },
                },
                "required": ["object_name", "frame", "property"],
            },
        ),
        types.Tool(
            name="set_frame",
            description="Cambia el frame actual de la timeline de Blender.",
            inputSchema={
                "type": "object",
                "properties": {
                    "frame": {"type": "integer", "description": "Número de frame al que ir"}
                },
                "required": ["frame"],
            },
        ),
        types.Tool(
            name="set_timeline",
            description="Configura el rango de la timeline (frame inicio y frame fin).",
            inputSchema={
                "type": "object",
                "properties": {
                    "start_frame": {"type": "integer"},
                    "end_frame": {"type": "integer"},
                },
                "required": ["start_frame", "end_frame"],
            },
        ),

        # ── ANIMACIÓN AVANZADA ───────────────────────────────────

        types.Tool(
            name="set_interpolation",
            description=(
                "Cambia el tipo de interpolación de los keyframes de un objeto. "
                "Controla cómo se mueve entre keyframes: "
                "LINEAR (velocidad constante), BEZIER (suave por defecto), "
                "CONSTANT (salto brusco), EASE_IN (arranca lento), "
                "EASE_OUT (termina lento), EASE_IN_OUT (suave en ambos extremos), "
                "BOUNCE (rebote), ELASTIC (elástico), BACK (overshooting)."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string"},
                    "interpolation": {
                        "type": "string",
                        "enum": ["LINEAR","BEZIER","CONSTANT","EASE_IN","EASE_OUT","EASE_IN_OUT","BOUNCE","ELASTIC","BACK"],
                    },
                    "property": {
                        "type": "string",
                        "enum": ["location","rotation_euler","scale","all"],
                        "description": "Propiedad cuyos keyframes modificar (default: all)"
                    },
                },
                "required": ["object_name", "interpolation"],
            },
        ),
        types.Tool(
            name="create_armature",
            description=(
                "Crea una armadura (skeleton) para rigging de personajes. "
                "Puedes agregar huesos básicos con nombre y posición. "
                "Útil para animar personajes y objetos deformables."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "name": {"type": "string", "description": "Nombre de la armadura"},
                    "location": {"type": "array", "items": {"type": "number"}, "description": "Posición [x,y,z]"},
                    "bones": {
                        "type": "array",
                        "description": "Lista de huesos a agregar",
                        "items": {
                            "type": "object",
                            "properties": {
                                "name": {"type": "string"},
                                "head": {"type": "array", "items": {"type": "number"}, "description": "Posición base [x,y,z]"},
                                "tail": {"type": "array", "items": {"type": "number"}, "description": "Posición punta [x,y,z]"},
                                "parent": {"type": "string", "description": "Nombre del hueso padre (opcional)"},
                            },
                            "required": ["name", "head", "tail"],
                        },
                    },
                },
                "required": ["name"],
            },
        ),
        types.Tool(
            name="parent_to_armature",
            description="Vincula un objeto mesh a una armadura para que los huesos lo deformen.",
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string", "description": "Nombre del objeto mesh"},
                    "armature_name": {"type": "string", "description": "Nombre de la armadura"},
                    "method": {
                        "type": "string",
                        "enum": ["ARMATURE_AUTO", "ARMATURE_ENVELOPE", "ARMATURE_NAME"],
                        "description": "Método de peso: AUTO (automático recomendado), ENVELOPE, NAME"
                    },
                },
                "required": ["object_name", "armature_name"],
            },
        ),
        types.Tool(
            name="animate_material",
            description=(
                "Anima una propiedad de material en keyframes. "
                "Puede animar el color base, emisión, roughness o metallic de un material Principled BSDF."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string"},
                    "frame": {"type": "integer"},
                    "property": {
                        "type": "string",
                        "enum": ["base_color", "emission_color", "roughness", "metallic", "alpha"],
                        "description": "Propiedad del material a animar"
                    },
                    "value": {
                        "description": "Valor: [R,G,B,A] para colores, número (0.0-1.0) para roughness/metallic/alpha",
                    },
                },
                "required": ["object_name", "frame", "property", "value"],
            },
        ),
        types.Tool(
            name="nla_create_strip",
            description=(
                "Crea un strip en el NLA Editor a partir de la acción activa de un objeto. "
                "Permite combinar y reutilizar animaciones. "
                "Primero el objeto debe tener keyframes para poder crear el strip."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string"},
                    "strip_name": {"type": "string", "description": "Nombre del strip NLA"},
                    "start_frame": {"type": "integer", "description": "Frame donde empieza el strip"},
                },
                "required": ["object_name", "strip_name", "start_frame"],
            },
        ),
        types.Tool(
            name="nla_set_strip_properties",
            description="Modifica propiedades de un strip NLA: repeticiones, escala de tiempo, influencia.",
            inputSchema={
                "type": "object",
                "properties": {
                    "object_name": {"type": "string"},
                    "strip_name": {"type": "string"},
                    "repeat": {"type": "number", "description": "Número de repeticiones del strip"},
                    "scale": {"type": "number", "description": "Escala de tiempo (2.0 = doble de lento)"},
                    "influence": {"type": "number", "description": "Influencia del strip (0.0 a 1.0)"},
                },
                "required": ["object_name", "strip_name"],
            },
        ),
        types.Tool(
            name="add_driver",
            description=(
                "Agrega un driver que vincula una propiedad de un objeto con la de otro. "
                "Ejemplo: vincular la rotación de un objeto con la posición X de otro. "
                "Útil para crear rigs y mecanismos automáticos."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "target_object": {"type": "string", "description": "Objeto cuya propiedad será controlada"},
                    "target_property": {"type": "string", "description": "Propiedad a controlar, ej: 'location.x', 'rotation_euler.z', 'scale.x'"},
                    "source_object": {"type": "string", "description": "Objeto que controla el driver"},
                    "source_property": {"type": "string", "description": "Propiedad fuente, ej: 'location.x'"},
                    "expression": {"type": "string", "description": "Expresión Python del driver, ej: 'var * 2' o 'var / 3.14'"},
                },
                "required": ["target_object", "target_property", "source_object", "source_property"],
            },
        ),

        # ── ILUMINACIÓN ──────────────────────────────────────────

        types.Tool(
            name="create_light",
            description=(
                "Crea una luz en la escena. "
                "Tipos: POINT (omnidireccional), SUN (solar/direccional), "
                "SPOT (foco cónico), AREA (panel de luz suave)."
            ),
            inputSchema={
                "type": "object",
                "properties": {
                    "light_type": {
                        "type": "string",
                        "enum": ["POINT","SUN","SPOT","AREA"],
                    },
                    "name": {"type": "string"},
                    "location": {"type": "array", "items": {"type": "number"}},
                    "energy": {"type": "number", "description": "Intensidad de la luz (default 1000 para POINT/SPOT/AREA, 5 para SUN)"},
                    "color": {"type": "array", "items": {"type": "number"}, "description": "Color de la luz [R,G,B] de 0.0 a 1.0"},
                },
                "required": ["light_type"],
            },
        ),
        types.Tool(
            name="set_light_properties",
            description="Modifica las propiedades de una luz existente (energía, color, radio).",
            inputSchema={
                "type": "object",
                "properties": {
                    "light_name": {"type": "string"},
                    "energy": {"type": "number"},
                    "color": {"type": "array", "items": {"type": "number"}},
                    "radius": {"type": "number", "description": "Radio/tamaño de la luz (para luces suaves)"},
                },
                "required": ["light_name"],
            },
        ),

        # ── CÁMARA ───────────────────────────────────────────────

        types.Tool(
            name="set_camera",
            description="Mueve la cámara a una posición y hace que apunte a un objeto o coordenada.",
            inputSchema={
                "type": "object",
                "properties": {
                    "location": {"type": "array", "items": {"type": "number"}, "description": "Nueva posición [x,y,z] de la cámara"},
                    "look_at": {"type": "array", "items": {"type": "number"}, "description": "Punto [x,y,z] hacia donde apunta la cámara"},
                    "camera_name": {"type": "string", "description": "Nombre de la cámara (default: 'Camera')"},
                },
                "required": ["location"],
            },
        ),
        types.Tool(
            name="set_camera_fov",
            description="Cambia el campo de visión (FOV) o la distancia focal de la cámara.",
            inputSchema={
                "type": "object",
                "properties": {
                    "focal_length": {"type": "number", "description": "Distancia focal en mm (ej: 50 = normal, 24 = gran angular, 200 = telefoto)"},
                    "camera_name": {"type": "string"},
                },
                "required": ["focal_length"],
            },
        ),

        # ── HISTORIAL ────────────────────────────────────────────

        types.Tool(
            name="get_history",
            description="Muestra el historial de comandos enviados a Blender, estados de escena y errores.",
            inputSchema={
                "type": "object",
                "properties": {
                    "limit": {"type": "integer", "description": "Cuántos eventos mostrar (default 20)"},
                    "filter_type": {
                        "type": "string",
                        "enum": ["comando","escena","error","todos"],
                        "description": "Filtrar por tipo de evento"
                    },
                },
                "required": [],
            },
        ),
        types.Tool(
            name="clear_history",
            description="Borra el historial de eventos guardado.",
            inputSchema={"type": "object", "properties": {}, "required": []},
        ),
    ]


# ============================================================
#  MANEJADOR DE HERRAMIENTAS
# ============================================================
@server.call_tool()
async def call_tool(name: str, arguments: dict) -> list[types.TextContent]:

    result = {}

    # ── ORIGINALES ───────────────────────────────────────────────

    if name == "get_scene_info":
        result = send_to_blender({"action": "get_scene_info"})
        if "error" not in result:
            log_event("escena", {"herramienta": name, "estado": result})

    elif name == "get_objects":
        result = send_to_blender({"action": "get_objects"})
        if "error" not in result:
            log_event("escena", {"herramienta": name, "cantidad_objetos": len(result.get("objects", []))})

    elif name == "get_active_object":
        result = send_to_blender({"action": "get_active_object"})

    elif name == "execute_code":
        code = arguments.get("code", "")
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": "execute_code", "codigo": code[:200]})
        if "error" in result:
            log_event("error", {"herramienta": "execute_code", "error": result["error"]})

    elif name == "create_object":
        obj_type = arguments["object_type"]
        obj_name = arguments.get("name", "")
        location = arguments.get("location", [0, 0, 0])
        x, y, z = location[0], location[1], location[2]
        ops_map = {
            "CUBE": "bpy.ops.mesh.primitive_cube_add",
            "SPHERE": "bpy.ops.mesh.primitive_uv_sphere_add",
            "CYLINDER": "bpy.ops.mesh.primitive_cylinder_add",
            "PLANE": "bpy.ops.mesh.primitive_plane_add",
            "CONE": "bpy.ops.mesh.primitive_cone_add",
            "TORUS": "bpy.ops.mesh.primitive_torus_add",
            "MONKEY": "bpy.ops.mesh.primitive_monkey_add",
        }
        op = ops_map.get(obj_type)
        code = f"{op}(location=({x}, {y}, {z}))\n"
        if obj_name:
            code += f"bpy.context.active_object.name = '{obj_name}'\n"
        code += "__result__ = bpy.context.active_object.name"
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "tipo": obj_type, "nombre": obj_name, "posicion": [x, y, z]})

    elif name == "set_material":
        obj_name = arguments["object_name"]
        color = arguments["color"]
        mat_name = arguments.get("material_name", f"Mat_{obj_name}")
        r, g, b = color[0], color[1], color[2]
        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
else:
    mat = bpy.data.materials.get('{mat_name}') or bpy.data.materials.new('{mat_name}')
    mat.use_nodes = True
    mat.node_tree.nodes.clear()
    bsdf = mat.node_tree.nodes.new('ShaderNodeBsdfPrincipled')
    output = mat.node_tree.nodes.new('ShaderNodeOutputMaterial')
    mat.node_tree.links.new(bsdf.outputs['BSDF'], output.inputs['Surface'])
    bsdf.inputs['Base Color'].default_value = ({r}, {g}, {b}, 1.0)
    if obj.data.materials:
        obj.data.materials[0] = mat
    else:
        obj.data.materials.append(mat)
    __result__ = f"Material '{mat_name}' aplicado a '{obj_name}'"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "color": [r, g, b]})

    elif name == "delete_object":
        obj_name = arguments["object_name"]
        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
else:
    bpy.data.objects.remove(obj, do_unlink=True)
    __result__ = "Objeto '{obj_name}' eliminado"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name})

    elif name == "move_object":
        obj_name = arguments["object_name"]
        location = arguments["location"]
        x, y, z = location[0], location[1], location[2]
        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
else:
    obj.location = ({x}, {y}, {z})
    __result__ = f"'{obj_name}' movido a ({x}, {y}, {z})"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "posicion": [x, y, z]})

    elif name == "render_scene":
        output_path = arguments.get("output_path", "")
        code = f"""
import bpy
scene = bpy.context.scene
{'scene.render.filepath = "' + output_path + '"' if output_path else ''}
bpy.ops.render.render(write_still=True)
__result__ = f"Render completado. Guardado en: {{scene.render.filepath}}"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "output_path": output_path})

    # ── MODIFICADORES ────────────────────────────────────────────

    elif name == "add_modifier":
        obj_name = arguments["object_name"]
        mod_type = arguments["modifier_type"]
        settings = arguments.get("settings", {})

        # Construimos el código base para agregar el modificador
        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
else:
    bpy.context.view_layer.objects.active = obj
    mod = obj.modifiers.new(name='{mod_type}', type='{mod_type}')
"""
        # Aplicamos configuraciones específicas según el tipo
        if mod_type == "SUBSURF" and "levels" in settings:
            code += f"    mod.levels = {settings['levels']}\n"
            code += f"    mod.render_levels = {settings.get('render_levels', settings['levels'])}\n"
        elif mod_type == "BEVEL":
            if "width" in settings:
                code += f"    mod.width = {settings['width']}\n"
            if "segments" in settings:
                code += f"    mod.segments = {settings['segments']}\n"
        elif mod_type == "SOLIDIFY" and "thickness" in settings:
            code += f"    mod.thickness = {settings['thickness']}\n"
        elif mod_type == "ARRAY":
            if "count" in settings:
                code += f"    mod.count = {settings['count']}\n"
            if "offset_x" in settings:
                code += f"    mod.relative_offset_displace[0] = {settings['offset_x']}\n"
        elif mod_type == "DECIMATE" and "ratio" in settings:
            code += f"    mod.ratio = {settings['ratio']}\n"

        code += f"    __result__ = f\"Modificador '{mod_type}' agregado a '{obj_name}'\"\n"
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "modificador": mod_type, "settings": settings})

    elif name == "apply_modifier":
        obj_name = arguments["object_name"]
        mod_name = arguments["modifier_name"]
        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
else:
    bpy.context.view_layer.objects.active = obj
    if '{mod_name}' not in obj.modifiers:
        __result__ = "Error: modificador '{mod_name}' no encontrado en '{obj_name}'"
    else:
        bpy.ops.object.modifier_apply(modifier='{mod_name}')
        __result__ = "Modificador '{mod_name}' aplicado permanentemente"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "modificador": mod_name})

    # ── ANIMACIÓN ────────────────────────────────────────────────

    elif name == "set_keyframe":
        obj_name = arguments["object_name"]
        frame = arguments["frame"]
        prop = arguments["property"]
        values = arguments.get("values", None)

        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
else:
    bpy.context.scene.frame_set({frame})
"""
        # Si se dieron valores, los asignamos antes del keyframe
        if values and len(values) == 3:
            if prop == "location":
                code += f"    obj.location = ({values[0]}, {values[1]}, {values[2]})\n"
            elif prop == "rotation":
                code += f"    obj.rotation_euler = ({values[0]}, {values[1]}, {values[2]})\n"
            elif prop == "scale":
                code += f"    obj.scale = ({values[0]}, {values[1]}, {values[2]})\n"

        # Insertamos el keyframe
        if prop == "all":
            code += "    obj.keyframe_insert(data_path='location')\n"
            code += "    obj.keyframe_insert(data_path='rotation_euler')\n"
            code += "    obj.keyframe_insert(data_path='scale')\n"
        elif prop == "location":
            code += "    obj.keyframe_insert(data_path='location')\n"
        elif prop == "rotation":
            code += "    obj.keyframe_insert(data_path='rotation_euler')\n"
        elif prop == "scale":
            code += "    obj.keyframe_insert(data_path='scale')\n"

        code += f"    __result__ = \"Keyframe insertado en frame {frame} para '{obj_name}' ({prop})\"\n"
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "frame": frame, "propiedad": prop})

    elif name == "set_frame":
        frame = arguments["frame"]
        code = f"""
import bpy
bpy.context.scene.frame_set({frame})
__result__ = f"Frame actual: {frame}"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "frame": frame})

    elif name == "set_timeline":
        start = arguments["start_frame"]
        end = arguments["end_frame"]
        code = f"""
import bpy
bpy.context.scene.frame_start = {start}
bpy.context.scene.frame_end = {end}
__result__ = f"Timeline configurada: frame {start} a {end}"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "inicio": start, "fin": end})

    # ── ILUMINACIÓN ──────────────────────────────────────────────

    elif name == "create_light":
        light_type = arguments["light_type"]
        light_name = arguments.get("name", f"Light_{light_type}")
        location = arguments.get("location", [0, 0, 3])
        # Energía por defecto según tipo
        default_energy = 5 if light_type == "SUN" else 1000
        energy = arguments.get("energy", default_energy)
        color = arguments.get("color", [1.0, 1.0, 1.0])
        x, y, z = location[0], location[1], location[2]
        r, g, b = color[0], color[1], color[2]

        code = f"""
import bpy
bpy.ops.object.light_add(type='{light_type}', location=({x}, {y}, {z}))
light_obj = bpy.context.active_object
light_obj.name = '{light_name}'
light_obj.data.name = '{light_name}'
light_obj.data.energy = {energy}
light_obj.data.color = ({r}, {g}, {b})
__result__ = f"Luz '{light_name}' ({light_type}) creada en ({x}, {y}, {z}) con energía {energy}"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "tipo": light_type, "nombre": light_name, "posicion": [x, y, z]})

    elif name == "set_light_properties":
        light_name = arguments["light_name"]
        energy = arguments.get("energy", None)
        color = arguments.get("color", None)
        radius = arguments.get("radius", None)

        code = f"""
import bpy
obj = bpy.data.objects.get('{light_name}')
if obj is None or obj.type != 'LIGHT':
    __result__ = "Error: luz '{light_name}' no encontrada"
else:
    light = obj.data
"""
        if energy is not None:
            code += f"    light.energy = {energy}\n"
        if color is not None:
            code += f"    light.color = ({color[0]}, {color[1]}, {color[2]})\n"
        if radius is not None:
            code += f"    light.shadow_soft_size = {radius}\n"
        code += f"    __result__ = \"Propiedades de '{light_name}' actualizadas\"\n"
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "luz": light_name})

    # ── CÁMARA ───────────────────────────────────────────────────

    elif name == "set_camera":
        location = arguments["location"]
        look_at = arguments.get("look_at", None)
        cam_name = arguments.get("camera_name", "Camera")
        x, y, z = location[0], location[1], location[2]

        code = f"""
import bpy
import mathutils
cam_obj = bpy.data.objects.get('{cam_name}')
if cam_obj is None:
    __result__ = "Error: cámara '{cam_name}' no encontrada"
else:
    cam_obj.location = ({x}, {y}, {z})
"""
        if look_at:
            lx, ly, lz = look_at[0], look_at[1], look_at[2]
            code += f"""
    # Hacemos que la cámara apunte al punto indicado
    direction = mathutils.Vector(({lx}, {ly}, {lz})) - mathutils.Vector(({x}, {y}, {z}))
    rot_quat = direction.to_track_quat('-Z', 'Y')
    cam_obj.rotation_euler = rot_quat.to_euler()
"""
        code += f"    __result__ = \"Cámara '{cam_name}' movida a ({x}, {y}, {z})\"\n"
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "posicion": [x, y, z], "mirar_a": look_at})

    elif name == "set_camera_fov":
        focal_length = arguments["focal_length"]
        cam_name = arguments.get("camera_name", "Camera")
        code = f"""
import bpy
cam_obj = bpy.data.objects.get('{cam_name}')
if cam_obj is None or cam_obj.type != 'CAMERA':
    __result__ = "Error: cámara '{cam_name}' no encontrada"
else:
    cam_obj.data.lens = {focal_length}
    __result__ = f"Focal length de '{cam_name}' cambiado a {focal_length}mm"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "focal_length": focal_length})

    # ── HISTORIAL ────────────────────────────────────────────────

    elif name == "get_history":
        limit = arguments.get("limit", 20)
        filter_type = arguments.get("filter_type", "todos")

        history = load_history()

        if filter_type != "todos":
            history = [e for e in history if e.get("tipo") == filter_type]

        # Retornamos los últimos N eventos
        recent = history[-limit:]
        result = {
            "total_eventos": len(history),
            "mostrando": len(recent),
            "eventos": recent
        }

    elif name == "clear_history":
        save_history([])
        result = {"success": True, "message": "Historial borrado correctamente"}

    # ── ANIMACIÓN AVANZADA ───────────────────────────────────────

    elif name == "set_interpolation":
        obj_name = arguments["object_name"]
        interp = arguments["interpolation"]
        prop = arguments.get("property", "all")

        # Mapeamos nombres amigables a data_path de fcurve
        prop_map = {
            "location": "location",
            "rotation_euler": "rotation_euler",
            "scale": "scale",
            "all": None,
        }
        data_path = prop_map.get(prop, None)

        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
elif obj.animation_data is None or obj.animation_data.action is None:
    __result__ = "Error: '{obj_name}' no tiene keyframes"
else:
    action = obj.animation_data.action
    count = 0
    for fcurve in action.fcurves:
        if '{data_path}' == 'None' or fcurve.data_path == '{data_path}':
            for kp in fcurve.keyframe_points:
                kp.interpolation = '{interp}'
                count += 1
    # Para tipos no-BEZIER no hay handles, limpiamos handles si aplica
    fcurve.update() if action.fcurves else None
    __result__ = f"Interpolación '{interp}' aplicada a {{count}} keyframes de '{obj_name}'"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "interpolacion": interp})

    elif name == "create_armature":
        arm_name = arguments["name"]
        location = arguments.get("location", [0, 0, 0])
        bones = arguments.get("bones", [])
        x, y, z = location[0], location[1], location[2]

        code = f"""
import bpy
# Crear armadura
bpy.ops.object.armature_add(enter_editmode=True, location=({x}, {y}, {z}))
arm_obj = bpy.context.active_object
arm_obj.name = '{arm_name}'
arm_obj.data.name = '{arm_name}'

# Eliminar el hueso por defecto
bpy.ops.armature.select_all(action='SELECT')
bpy.ops.armature.delete()

# Agregar huesos personalizados
bone_dict = {json.dumps(bones)}
created = []
for b in bone_dict:
    bone = arm_obj.data.edit_bones.new(b['name'])
    h = b['head']
    t = b['tail']
    bone.head = (h[0], h[1], h[2])
    bone.tail = (t[0], t[1], t[2])
    created.append(b['name'])

# Asignar padres
for b in bone_dict:
    if 'parent' in b and b['parent']:
        child = arm_obj.data.edit_bones.get(b['name'])
        parent = arm_obj.data.edit_bones.get(b['parent'])
        if child and parent:
            child.parent = parent
            child.use_connect = False

bpy.ops.object.mode_set(mode='OBJECT')
__result__ = f"Armadura '{arm_name}' creada con huesos: {{created}}"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "armadura": arm_name, "huesos": len(bones)})

    elif name == "parent_to_armature":
        obj_name = arguments["object_name"]
        arm_name = arguments["armature_name"]
        method = arguments.get("method", "ARMATURE_AUTO")

        code = f"""
import bpy
mesh_obj = bpy.data.objects.get('{obj_name}')
arm_obj = bpy.data.objects.get('{arm_name}')
if mesh_obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
elif arm_obj is None:
    __result__ = "Error: armadura '{arm_name}' no encontrada"
elif arm_obj.type != 'ARMATURE':
    __result__ = "Error: '{arm_name}' no es una armadura"
else:
    # Deseleccionar todo
    bpy.ops.object.select_all(action='DESELECT')
    # Seleccionar mesh primero, luego armadura (el orden importa)
    mesh_obj.select_set(True)
    arm_obj.select_set(True)
    bpy.context.view_layer.objects.active = arm_obj
    bpy.ops.object.parent_set(type='{method}')
    __result__ = f"'{obj_name}' vinculado a armadura '{arm_name}' con método '{method}'"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "armadura": arm_name})

    elif name == "animate_material":
        obj_name = arguments["object_name"]
        frame = arguments["frame"]
        prop = arguments["property"]
        value = arguments["value"]

        # Mapeamos nombre amigable al input del Principled BSDF
        input_map = {
            "base_color": "Base Color",
            "emission_color": "Emission Color",
            "roughness": "Roughness",
            "metallic": "Metallic",
            "alpha": "Alpha",
        }
        input_name = input_map.get(prop, prop)
        is_color = prop in ["base_color", "emission_color"]

        if is_color and isinstance(value, list):
            # Aseguramos que el color tenga 4 componentes RGBA
            while len(value) < 4:
                value.append(1.0)
            value_code = f"({value[0]}, {value[1]}, {value[2]}, {value[3]})"
        else:
            value_code = str(value)

        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
elif not obj.material_slots:
    __result__ = "Error: '{obj_name}' no tiene materiales"
else:
    mat = obj.material_slots[0].material
    if not mat or not mat.use_nodes:
        __result__ = "Error: el material no usa nodos"
    else:
        bsdf = None
        for node in mat.node_tree.nodes:
            if node.type == 'BSDF_PRINCIPLED':
                bsdf = node
                break
        if bsdf is None:
            __result__ = "Error: no se encontró nodo Principled BSDF"
        else:
            bpy.context.scene.frame_set({frame})
            inp = bsdf.inputs.get('{input_name}')
            if inp is None:
                __result__ = "Error: propiedad '{input_name}' no encontrada"
            else:
                inp.default_value = {value_code}
                inp.keyframe_insert(data_path='default_value', frame={frame})
                __result__ = f"Keyframe de material insertado en frame {frame}: '{input_name}' = {value_code}"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "propiedad": prop, "frame": frame})

    elif name == "nla_create_strip":
        obj_name = arguments["object_name"]
        strip_name = arguments["strip_name"]
        start_frame = arguments["start_frame"]

        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
elif obj.animation_data is None or obj.animation_data.action is None:
    __result__ = "Error: '{obj_name}' no tiene una acción activa con keyframes"
else:
    anim_data = obj.animation_data
    action = anim_data.action
    # Guardamos la acción antes de pasarla al NLA
    action.name = '{strip_name}_action'
    track = anim_data.nla_tracks.new()
    track.name = '{strip_name}_track'
    strip = track.strips.new('{strip_name}', {start_frame}, action)
    # Desvinculamos la acción del slot principal para que el NLA tome control
    anim_data.action = None
    __result__ = f"Strip NLA '{strip_name}' creado en frame {start_frame} para '{obj_name}'"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "strip": strip_name})

    elif name == "nla_set_strip_properties":
        obj_name = arguments["object_name"]
        strip_name = arguments["strip_name"]
        repeat = arguments.get("repeat", None)
        scale = arguments.get("scale", None)
        influence = arguments.get("influence", None)

        code = f"""
import bpy
obj = bpy.data.objects.get('{obj_name}')
if obj is None:
    __result__ = "Error: objeto '{obj_name}' no encontrado"
elif obj.animation_data is None:
    __result__ = "Error: '{obj_name}' no tiene datos de animación"
else:
    strip = None
    for track in obj.animation_data.nla_tracks:
        for s in track.strips:
            if s.name == '{strip_name}':
                strip = s
                break
    if strip is None:
        __result__ = "Error: strip '{strip_name}' no encontrado en '{obj_name}'"
    else:
        changes = []
        {'strip.repeat = ' + str(repeat) + '; changes.append("repeat=" + str(' + str(repeat) + '))' if repeat is not None else ''}
        {'strip.scale = ' + str(scale) + '; changes.append("scale=" + str(' + str(scale) + '))' if scale is not None else ''}
        {'strip.influence = ' + str(influence) + '; changes.append("influence=" + str(' + str(influence) + '))' if influence is not None else ''}
        __result__ = f"Strip '{strip_name}' actualizado: {{', '.join(changes)}}"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "objeto": obj_name, "strip": strip_name})

    elif name == "add_driver":
        target_obj = arguments["target_object"]
        target_prop = arguments["target_property"]
        source_obj = arguments["source_object"]
        source_prop = arguments["source_property"]
        expression = arguments.get("expression", "var")

        # Separamos data_path e index (ej: "location.x" → data_path="location", index=0)
        prop_index_map = {"x": 0, "y": 1, "z": 2}

        code = f"""
import bpy

target = bpy.data.objects.get('{target_obj}')
source = bpy.data.objects.get('{source_obj}')

if target is None:
    __result__ = "Error: objeto destino '{target_obj}' no encontrado"
elif source is None:
    __result__ = "Error: objeto fuente '{source_obj}' no encontrado"
else:
    # Parseamos la propiedad objetivo
    target_prop_str = '{target_prop}'
    parts = target_prop_str.split('.')
    data_path = parts[0]
    index_map = {{'x': 0, 'y': 1, 'z': 2}}
    arr_index = index_map.get(parts[1], 0) if len(parts) > 1 else -1

    # Parseamos la propiedad fuente
    source_prop_str = '{source_prop}'
    src_parts = source_prop_str.split('.')
    src_data_path = src_parts[0]
    src_index = index_map.get(src_parts[1], 0) if len(src_parts) > 1 else 0

    try:
        # Agregamos el driver
        driver_obj = target.driver_add(data_path, arr_index)
        driver = driver_obj.driver
        driver.type = 'SCRIPTED'
        driver.expression = '{expression}'

        # Configuramos la variable del driver
        var = driver.variables.new()
        var.name = 'var'
        var.type = 'TRANSFORMS'
        target_var = var.targets[0]
        target_var.id = source
        target_var.transform_type = 'LOC_X' if src_data_path == 'location' and src_index == 0 else \
                                    'LOC_Y' if src_data_path == 'location' and src_index == 1 else \
                                    'LOC_Z' if src_data_path == 'location' and src_index == 2 else \
                                    'ROT_X' if src_data_path == 'rotation_euler' and src_index == 0 else \
                                    'ROT_Y' if src_data_path == 'rotation_euler' and src_index == 1 else \
                                    'ROT_Z' if src_data_path == 'rotation_euler' and src_index == 2 else \
                                    'SCALE_X' if src_data_path == 'scale' and src_index == 0 else \
                                    'SCALE_Y' if src_data_path == 'scale' and src_index == 1 else \
                                    'SCALE_Z'
        __result__ = f"Driver creado: '{target_obj}.{target_prop}' controlado por '{source_obj}.{source_prop}' (expr: '{expression}')"
    except Exception as e:
        __result__ = f"Error creando driver: {{str(e)}}"
"""
        result = send_to_blender({"action": "execute_code", "code": code})
        log_event("comando", {"herramienta": name, "destino": target_obj, "fuente": source_obj})

    else:
        result = {"error": f"Herramienta desconocida: '{name}'"}
        log_event("error", {"herramienta": name, "error": "Herramienta desconocida"})

    # Registramos errores de Blender automáticamente
    if isinstance(result, dict) and "error" in result:
        log_event("error", {"herramienta": name, "error": result["error"]})

    result_text = json.dumps(result, indent=2, ensure_ascii=False)
    return [types.TextContent(type="text", text=result_text)]


# ============================================================
#  PUNTO DE ENTRADA
# ============================================================
async def main():
    print("[MCP] Servidor Blender-MCP v3.0 iniciado")
    print(f"[MCP] Historial guardado en: {HISTORY_FILE}")
    async with stdio_server() as (read_stream, write_stream):
        await server.run(read_stream, write_stream, server.create_initialization_options())

if __name__ == "__main__":
    asyncio.run(main())
