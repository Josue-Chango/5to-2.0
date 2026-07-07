<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Habitaciones - HotelGestión</title>
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
            <div class="page-header d-flex justify-content-between align-items-center">
                <div>
                    <h1><i class="fas fa-bed me-2" style="color: var(--gold);"></i>Habitaciones</h1>
                    <p class="subtitle">Gestione las habitaciones del hotel</p>
                </div>
                <a href="?action=nuevo" class="btn btn-primary">
                    <i class="fas fa-plus me-1"></i> Nueva Habitación
                </a>
            </div>

            <div class="table-container">
                <div class="search-box mb-3">
                    <i class="fas fa-search"></i>
                    <input type="text" class="form-control" id="tableSearch" placeholder="Buscar habitaciones..." onkeyup="filterTable()">
                </div>
                <table class="table" id="dataTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Número</th>
                            <th>Piso</th>
                            <th>Estado</th>
                            <th>Tipo</th>
                            <th class="text-center">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <c:forEach var="h" items="${habitaciones}">
                            <tr>
                                <td><span class="fw-semibold">${h.idHabitacion}</span></td>
                                <td><i class="fas fa-door-open me-1 text-muted"></i>${h.numero}</td>
                                <td><i class="fas fa-building me-1 text-muted"></i>Piso ${h.piso}</td>
                                <td>
                                    <span class="badge ${h.estado == 'Disponible' ? 'bg-success' : h.estado == 'Ocupada' ? 'bg-danger' : 'bg-warning'}">
                                        <i class="fas ${h.estado == 'Disponible' ? 'fa-check-circle' : h.estado == 'Ocupada' ? 'fa-times-circle' : 'fa-tools'} me-1"></i>
                                        ${h.estado}
                                    </span>
                                </td>
                                <td>
                                    <c:forEach var="t" items="${tipos}">
                                        <c:if test="${t.idTipo == h.idTipo}">${t.nombre}</c:if>
                                    </c:forEach>
                                </td>
                                <td class="text-center">
                                    <a href="?action=editar&id=${h.idHabitacion}" class="btn btn-sm btn-warning me-1" title="Editar">
                                        <i class="fas fa-edit"></i>
                                    </a>
                                    <a href="javascript:void(0)" onclick="confirmDelete(${h.idHabitacion}, '${h.numero}')" class="btn btn-sm btn-danger" title="Eliminar">
                                        <i class="fas fa-trash"></i>
                                    </a>
                                </td>
                            </tr>
                        </c:forEach>
                    </tbody>
                </table>
                <c:if test="${empty habitaciones}">
                    <div class="text-center py-4 text-muted">
                        <i class="fas fa-door-open fa-3x mb-3" style="color: var(--gold);"></i>
                        <p class="mb-0">No hay habitaciones registradas</p>
                    </div>
                </c:if>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"><i class="fas fa-exclamation-triangle me-2"></i>Confirmar Eliminación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="modal-icon"><i class="fas fa-trash-alt"></i></div>
                    <p class="fw-semibold mb-1">¿Está seguro de eliminar este registro?</p>
                    <p class="text-muted small mb-0" id="deleteItemInfo">Esta acción no se puede deshacer.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="fas fa-times me-1"></i> Cancelar
                    </button>
                    <a href="#" id="confirmDeleteBtn" class="btn btn-danger">
                        <i class="fas fa-trash me-1"></i> Eliminar
                    </a>
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
    <script>
        function filterTable() {
            const input = document.getElementById('tableSearch');
            const filter = input.value.toLowerCase();
            const rows = document.querySelectorAll('#dataTable tbody tr');
            rows.forEach(row => {
                const text = row.textContent.toLowerCase();
                row.style.display = text.includes(filter) ? '' : 'none';
            });
        }

        function confirmDelete(id, info) {
            document.getElementById('deleteItemInfo').textContent = 'ID: ' + id + ' - ' + info;
            document.getElementById('confirmDeleteBtn').href = '?action=eliminar&id=' + id;
            const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
            modal.show();
        }
    </script>
</body>
</html>
