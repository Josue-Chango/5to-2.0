<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>${habitacion == null ? 'Nueva' : 'Editar'} Habitación - HotelGestión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    <link href="${pageContext.request.contextPath}/css/style.css" rel="stylesheet">
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-custom">
        <div class="container">
            <a class="navbar-brand" href="${pageContext.request.contextPath}/">
                <i class="fas fa-hotel"></i> HotelGestión
            </a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/tipoHabitacion"><i class="fas fa-layer-group"></i>Tipos</a></li>
                    <li class="nav-item"><a class="nav-link active" href="${pageContext.request.contextPath}/habitacion"><i class="fas fa-bed"></i>Habitaciones</a></li>
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/cliente"><i class="fas fa-users"></i>Clientes</a></li>
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/reserva"><i class="fas fa-calendar-check"></i>Reservas</a></li>
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/gasto"><i class="fas fa-coins"></i>Gastos</a></li>
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/servicio"><i class="fas fa-concierge-bell"></i>Servicios</a></li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="content-wrapper">
        <div class="container">
            <div class="page-header">
                <h1>
                    <i class="fas ${habitacion == null ? 'fa-plus-circle' : 'fa-edit'} me-2" style="color: var(--gold);"></i>
                    ${habitacion == null ? 'Nueva' : 'Editar'} Habitación
                </h1>
                <p class="subtitle">${habitacion == null ? 'Complete el formulario para registrar una nueva habitación' : 'Modifique los datos de la habitación'}</p>
            </div>

            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-header">
                            <i class="fas fa-bed me-2"></i> Datos de la Habitación
                        </div>
                        <div class="card-body p-4">
                            <form action="" method="post">
                                <c:if test="${habitacion != null}">
                                    <input type="hidden" name="id" value="${habitacion.idHabitacion}">
                                    <input type="hidden" name="action" value="editar">
                                </c:if>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-door-open me-1" style="color: var(--gold);"></i>Número</label>
                                        <input type="text" name="numero" class="form-control" value="${habitacion.numero}" required>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-building me-1" style="color: var(--gold);"></i>Piso</label>
                                        <input type="number" name="piso" class="form-control" value="${habitacion.piso}" required>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-info-circle me-1" style="color: var(--gold);"></i>Estado</label>
                                        <select name="estado" class="form-select">
                                            <option value="Disponible" ${habitacion != null && habitacion.estado == 'Disponible' ? 'selected' : ''}>Disponible</option>
                                            <option value="Ocupada" ${habitacion != null && habitacion.estado == 'Ocupada' ? 'selected' : ''}>Ocupada</option>
                                            <option value="Mantenimiento" ${habitacion != null && habitacion.estado == 'Mantenimiento' ? 'selected' : ''}>Mantenimiento</option>
                                        </select>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-layer-group me-1" style="color: var(--gold);"></i>Tipo</label>
                                        <select name="idTipo" class="form-select">
                                            <c:forEach var="t" items="${tipos}">
                                                <option value="${t.idTipo}" ${habitacion != null && habitacion.idTipo == t.idTipo ? 'selected' : ''}>${t.nombre}</option>
                                            </c:forEach>
                                        </select>
                                    </div>
                                </div>
                                <div class="mt-4 d-flex gap-2">
                                    <button type="submit" class="btn btn-success">
                                        <i class="fas fa-save me-1"></i> Guardar
                                    </button>
                                    <a href="${pageContext.request.contextPath}/habitacion" class="btn btn-secondary">
                                        <i class="fas fa-arrow-left me-1"></i> Cancelar
                                    </a>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <footer class="footer-custom">
        <div class="container">
            <div class="footer-brand"><i class="fas fa-hotel me-2"></i>HotelGestión</div>
            <div class="footer-divider"></div>
            <p>&copy; 2024 HotelGestión. Todos los derechos reservados.</p>
        </div>
    </footer>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
