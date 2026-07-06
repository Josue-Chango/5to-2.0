<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>${reserva == null ? 'Nueva' : 'Editar'} Reserva - HotelGestión</title>
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
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/cliente"><i class="fas fa-users"></i>Clientes</a></li>
                    <li class="nav-item"><a class="nav-link active" href="${pageContext.request.contextPath}/reserva"><i class="fas fa-calendar-check"></i>Reservas</a></li>
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
                    <i class="fas ${reserva == null ? 'fa-plus-circle' : 'fa-edit'} me-2" style="color: var(--gold);"></i>
                    ${reserva == null ? 'Nueva' : 'Editar'} Reserva
                </h1>
                <p class="subtitle">${reserva == null ? 'Registre una nueva reserva en el sistema' : 'Modifique los datos de la reserva'}</p>
            </div>

            <div class="row justify-content-center">
                <div class="col-lg-10">
                    <div class="card">
                        <div class="card-header">
                            <i class="fas fa-calendar-check me-2"></i> Datos de la Reserva
                        </div>
                        <div class="card-body p-4">
                            <c:if test="${not empty errores}">
                                <div class="alert alert-danger alert-dismissible fade show" role="alert">
                                    <i class="fas fa-exclamation-circle me-2"></i>
                                    <strong>Errores:</strong>
                                    <ul class="mb-0 mt-1">
                                        <c:forEach var="err" items="${errores}">
                                            <li>${err}</li>
                                        </c:forEach>
                                    </ul>
                                    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                                </div>
                            </c:if>
                            <form action="" method="post">
                                <c:if test="${reserva != null}">
                                    <input type="hidden" name="id" value="${reserva.idReserva}">
                                    <input type="hidden" name="action" value="editar">
                                </c:if>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-user me-1" style="color: var(--gold);"></i>Cliente</label>
                                        <select name="idCliente" class="form-select" required>
                                            <option value="">Seleccione un cliente...</option>
                                            <c:forEach var="c" items="${clientes}">
                                                <option value="${c.idCliente}" ${reserva != null && reserva.idCliente == c.idCliente ? 'selected' : ''}>${c.nombre} ${c.apellido}</option>
                                            </c:forEach>
                                        </select>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-bed me-1" style="color: var(--gold);"></i>Habitación</label>
                                        <select name="idHabitacion" class="form-select" required>
                                            <option value="">Seleccione una habitación...</option>
                                            <c:forEach var="h" items="${habitaciones}">
                                                <option value="${h.idHabitacion}" ${reserva != null && reserva.idHabitacion == h.idHabitacion ? 'selected' : ''}>${h.numero} - Piso ${h.piso}</option>
                                            </c:forEach>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label"><i class="fas fa-calendar-plus me-1" style="color: var(--gold);"></i>Fecha Entrada</label>
                                        <input type="date" name="fechaEntrada" class="form-control" value="${reserva != null ? reserva.fechaEntrada : param.fechaEntrada}" required>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label"><i class="fas fa-calendar-minus me-1" style="color: var(--gold);"></i>Fecha Salida</label>
                                        <input type="date" name="fechaSalida" class="form-control" value="${reserva != null ? reserva.fechaSalida : param.fechaSalida}" required>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label"><i class="fas fa-info-circle me-1" style="color: var(--gold);"></i>Estado</label>
                                        <select name="estado" class="form-select">
                                            <option value="Confirmada" ${reserva != null && reserva.estado == 'Confirmada' ? 'selected' : ''}>Confirmada</option>
                                            <option value="Cancelada" ${reserva != null && reserva.estado == 'Cancelada' ? 'selected' : ''}>Cancelada</option>
                                            <option value="Finalizada" ${reserva != null && reserva.estado == 'Finalizada' ? 'selected' : ''}>Finalizada</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label"><i class="fas fa-concierge-bell me-1" style="color: var(--gold);"></i>Servicios Adicionales</label>
                                    <div class="row">
                                        <c:forEach var="s" items="${servicios}">
                                            <div class="col-md-3">
                                                <div class="form-check">
                                                    <input type="checkbox" name="servicios" value="${s.idServicio}" class="form-check-input"
                                                    ${reserva != null && reserva.hasServicio(s.idServicio) ? 'checked' : ''}>
                                                    <label class="form-check-label">${s.nombre} <span class="text-muted">($${s.precio})</span></label>
                                                </div>
                                            </div>
                                        </c:forEach>
                                    </div>
                                </div>
                                <div class="mt-4 d-flex gap-2">
                                    <button type="submit" class="btn btn-success">
                                        <i class="fas fa-save me-1"></i> Guardar
                                    </button>
                                    <a href="${pageContext.request.contextPath}/reserva" class="btn btn-secondary">
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
