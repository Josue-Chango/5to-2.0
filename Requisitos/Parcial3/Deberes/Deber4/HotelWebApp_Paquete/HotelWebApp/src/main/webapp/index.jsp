<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Hotel Gestión - Sistema Administrativo</title>
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
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/gasto"><i class="fas fa-coins"></i>Gastos</a></li>
                    <li class="nav-item"><a class="nav-link" href="${pageContext.request.contextPath}/servicio"><i class="fas fa-concierge-bell"></i>Servicios</a></li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="content-wrapper">
        <div class="container">
            <div class="page-header text-center">
                <h1><i class="fas fa-hotel me-2" style="color: var(--gold);"></i>Sistema de Gestión Hotelera</h1>
                <p class="subtitle">Bienvenido al panel de administración. Seleccione un módulo para comenzar.</p>
            </div>

            <div class="row g-4">
                <div class="col-md-4">
                    <div class="card card-module bg-navy text-white">
                        <div class="card-body text-center">
                            <div class="card-icon"><i class="fas fa-layer-group"></i></div>
                            <h5 class="card-title">Tipos de Habitación</h5>
                            <p class="card-text">Gestione las categorías: Simple, Doble, Suite y más.</p>
                            <a href="${pageContext.request.contextPath}/tipoHabitacion" class="btn btn-gold">
                                <i class="fas fa-arrow-right me-1"></i> Ir al Módulo
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-module bg-teal text-white">
                        <div class="card-body text-center">
                            <div class="card-icon"><i class="fas fa-bed"></i></div>
                            <h5 class="card-title">Habitaciones</h5>
                            <p class="card-text">Administre el inventario de habitaciones del hotel.</p>
                            <a href="${pageContext.request.contextPath}/habitacion" class="btn btn-gold">
                                <i class="fas fa-arrow-right me-1"></i> Ir al Módulo
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-module bg-rose text-white">
                        <div class="card-body text-center">
                            <div class="card-icon"><i class="fas fa-users"></i></div>
                            <h5 class="card-title">Clientes</h5>
                            <p class="card-text">Registre y administre los huéspedes del hotel.</p>
                            <a href="${pageContext.request.contextPath}/cliente" class="btn btn-gold">
                                <i class="fas fa-arrow-right me-1"></i> Ir al Módulo
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-module bg-gold text-white">
                        <div class="card-body text-center">
                            <div class="card-icon"><i class="fas fa-calendar-check"></i></div>
                            <h5 class="card-title">Reservas</h5>
                            <p class="card-text">Gestione las reservas y estadías de los clientes.</p>
                            <a href="${pageContext.request.contextPath}/reserva" class="btn btn-light">
                                <i class="fas fa-arrow-right me-1"></i> Ir al Módulo
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-module bg-amber text-white">
                        <div class="card-body text-center">
                            <div class="card-icon"><i class="fas fa-coins"></i></div>
                            <h5 class="card-title">Gastos</h5>
                            <p class="card-text">Controle los gastos y costos operativos del hotel.</p>
                            <a href="${pageContext.request.contextPath}/gasto" class="btn btn-gold">
                                <i class="fas fa-arrow-right me-1"></i> Ir al Módulo
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-module bg-slate text-white">
                        <div class="card-body text-center">
                            <div class="card-icon"><i class="fas fa-concierge-bell"></i></div>
                            <h5 class="card-title">Servicios</h5>
                            <p class="card-text">Administre los servicios adicionales del hotel.</p>
                            <a href="${pageContext.request.contextPath}/servicio" class="btn btn-gold">
                                <i class="fas fa-arrow-right me-1"></i> Ir al Módulo
                            </a>
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
