<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>${cliente == null ? 'Nuevo' : 'Editar'} Cliente - HotelGestión</title>
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
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/habitacion"><i class="fas fa-bed"></i>Habitaciones</a></li>
                    <li class="nav-item"><a class="nav-link active" href="${pageContext.request.contextPath}/cliente"><i class="fas fa-users"></i>Clientes</a></li>
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
                    <i class="fas ${cliente == null ? 'fa-user-plus' : 'fa-user-edit'} me-2" style="color: var(--gold);"></i>
                    ${cliente == null ? 'Nuevo' : 'Editar'} Cliente
                </h1>
                <p class="subtitle">${cliente == null ? 'Registre un nuevo huésped en el sistema' : 'Modifique los datos del cliente'}</p>
            </div>

            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-header">
                            <i class="fas fa-user me-2"></i> Datos del Cliente
                        </div>
                        <div class="card-body p-4">
                            <form action="" method="post">
                                <c:if test="${cliente != null}">
                                    <input type="hidden" name="id" value="${cliente.idCliente}">
                                    <input type="hidden" name="action" value="editar">
                                </c:if>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-user me-1" style="color: var(--gold);"></i>Nombre</label>
                                        <input type="text" name="nombre" class="form-control" value="${cliente.nombre}" required>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-user me-1" style="color: var(--gold);"></i>Apellido</label>
                                        <input type="text" name="apellido" class="form-control" value="${cliente.apellido}" required>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label"><i class="fas fa-id-card me-1" style="color: var(--gold);"></i>CEDULA</label>
                                        <input type="text" name="dni" class="form-control" value="${cliente.dni}" required>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label"><i class="fas fa-phone me-1" style="color: var(--gold);"></i>Teléfono</label>
                                        <input type="text" name="telefono" class="form-control" value="${cliente.telefono}">
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label"><i class="fas fa-envelope me-1" style="color: var(--gold);"></i>Email</label>
                                        <input type="email" name="email" class="form-control" value="${cliente.email}">
                                    </div>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label"><i class="fas fa-map-marker-alt me-1" style="color: var(--gold);"></i>Dirección</label>
                                    <input type="text" name="direccion" class="form-control" value="${cliente.direccion}">
                                </div>
                                <div class="mt-4 d-flex gap-2">
                                    <button type="submit" class="btn btn-success">
                                        <i class="fas fa-save me-1"></i> Guardar
                                    </button>
                                    <a href="${pageContext.request.contextPath}/cliente" class="btn btn-secondary">
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
