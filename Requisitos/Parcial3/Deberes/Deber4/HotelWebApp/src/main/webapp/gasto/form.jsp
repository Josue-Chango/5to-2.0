<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>${gasto == null ? 'Nuevo' : 'Editar'} Gasto - HotelGestión</title>
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
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/reserva"><i class="fas fa-calendar-check"></i>Reservas</a></li>
                    <li class="nav-item"><a class="nav-link active" href="${pageContext.request.contextPath}/gasto"><i class="fas fa-coins"></i>Gastos</a></li>
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/servicio"><i class="fas fa-concierge-bell"></i>Servicios</a></li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="content-wrapper">
        <div class="container">
            <div class="page-header">
                <h1>
                    <i class="fas ${gasto == null ? 'fa-plus-circle' : 'fa-edit'} me-2" style="color: var(--gold);"></i>
                    ${gasto == null ? 'Nuevo' : 'Editar'} Gasto
                </h1>
                <p class="subtitle">${gasto == null ? 'Registre un nuevo gasto en el sistema' : 'Modifique los datos del gasto'}</p>
            </div>

            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-header">
                            <i class="fas fa-coins me-2"></i> Datos del Gasto
                        </div>
                        <div class="card-body p-4">
                            <form action="" method="post">
                                <c:if test="${gasto != null}">
                                    <input type="hidden" name="id" value="${gasto.idGasto}">
                                    <input type="hidden" name="action" value="editar">
                                </c:if>
                                <div class="mb-3">
                                    <label class="form-label"><i class="fas fa-calendar-check me-1" style="color: var(--gold);"></i>Reserva</label>
                                    <select name="idReserva" class="form-select" required>
                                        <option value="">Seleccione una reserva...</option>
                                        <c:forEach var="r" items="${reservas}">
                                            <option value="${r.idReserva}" ${gasto != null && gasto.idReserva == r.idReserva ? 'selected' : ''}>Reserva #${r.idReserva}</option>
                                        </c:forEach>
                                    </select>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label"><i class="fas fa-align-left me-1" style="color: var(--gold);"></i>Descripción</label>
                                    <input type="text" name="descripcion" class="form-control" value="${gasto.descripcion}" required>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-dollar-sign me-1" style="color: var(--gold);"></i>Monto</label>
                                        <input type="number" step="0.01" name="monto" class="form-control" value="${gasto.monto}" required>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label"><i class="fas fa-calendar me-1" style="color: var(--gold);"></i>Fecha</label>
                                        <input type="date" name="fecha" class="form-control" value="${gasto.fecha}" required>
                                    </div>
                                </div>
                                <div class="mt-4 d-flex gap-2">
                                    <button type="submit" class="btn btn-success">
                                        <i class="fas fa-save me-1"></i> Guardar
                                    </button>
                                    <a href="${pageContext.request.contextPath}/gasto" class="btn btn-secondary">
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
