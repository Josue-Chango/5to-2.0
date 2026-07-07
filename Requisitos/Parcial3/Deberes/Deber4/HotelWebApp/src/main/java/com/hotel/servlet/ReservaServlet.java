package com.hotel.servlet;

import com.hotel.dao.ClienteDAO;
import com.hotel.dao.HabitacionDAO;
import com.hotel.dao.ReservaDAO;
import com.hotel.dao.ServicioDAO;
import com.hotel.model.Reserva;
import com.hotel.model.ReservaServicio;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

@WebServlet("/reserva")
public class ReservaServlet extends HttpServlet {

    private ReservaDAO reservaDAO = new ReservaDAO();
    private ClienteDAO clienteDAO = new ClienteDAO();
    private HabitacionDAO habitacionDAO = new HabitacionDAO();
    private ServicioDAO servicioDAO = new ServicioDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null || action.equals("listar")) {
            req.setAttribute("reservas", reservaDAO.listar());
            req.setAttribute("clientes", clienteDAO.listar());
            req.setAttribute("habitaciones", habitacionDAO.listar());
            req.getRequestDispatcher("/reserva/listar.jsp").forward(req, resp);
        } else if (action.equals("nuevo")) {
            req.setAttribute("clientes", clienteDAO.listar());
            req.setAttribute("habitaciones", habitacionDAO.listar());
            req.setAttribute("servicios", servicioDAO.listar());
            req.getRequestDispatcher("/reserva/form.jsp").forward(req, resp);
        } else if (action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Reserva r = reservaDAO.buscar(id);
            req.setAttribute("reserva", r);
            req.setAttribute("clientes", clienteDAO.listar());
            req.setAttribute("habitaciones", habitacionDAO.listar());
            req.setAttribute("servicios", servicioDAO.listar());
            req.getRequestDispatcher("/reserva/form.jsp").forward(req, resp);
        } else if (action.equals("eliminar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            reservaDAO.eliminar(id);
            resp.sendRedirect(req.getContextPath() + "/reserva");
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        int idCliente = Integer.parseInt(req.getParameter("idCliente"));
        int idHabitacion = Integer.parseInt(req.getParameter("idHabitacion"));
        LocalDate fechaEntrada = LocalDate.parse(req.getParameter("fechaEntrada"));
        LocalDate fechaSalida = LocalDate.parse(req.getParameter("fechaSalida"));
        String estado = req.getParameter("estado");

        List<String> errores = new ArrayList<>();
        if (fechaEntrada.isBefore(LocalDate.now())) {
            errores.add("La fecha de entrada no puede ser anterior a hoy.");
        }
        if (!fechaSalida.isAfter(fechaEntrada)) {
            errores.add("La fecha de salida debe ser posterior a la fecha de entrada.");
        }

        if (!errores.isEmpty()) {
            req.setAttribute("errores", errores);
            req.setAttribute("clientes", clienteDAO.listar());
            req.setAttribute("habitaciones", habitacionDAO.listar());
            req.setAttribute("servicios", servicioDAO.listar());
            req.getRequestDispatcher("/reserva/form.jsp").forward(req, resp);
            return;
        }

        String[] serviciosParam = req.getParameterValues("servicios");
        double totalServicios = 0.0;
        List<ReservaServicio> listaServicios = new ArrayList<>();
        if (serviciosParam != null) {
            for (String idServ : serviciosParam) {
                int idS = Integer.parseInt(idServ);
                var serv = servicioDAO.buscar(idS);
                if (serv != null) {
                    listaServicios.add(new ReservaServicio(0, idS, 1, serv.getPrecio()));
                    totalServicios += serv.getPrecio();
                }
            }
        }

        if (action != null && action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Reserva r = reservaDAO.buscar(id);
            if (r != null) {
                r.setIdCliente(idCliente);
                r.setIdHabitacion(idHabitacion);
                r.setFechaEntrada(fechaEntrada);
                r.setFechaSalida(fechaSalida);
                r.setEstado(estado);
                r.setServicios(listaServicios);
                r.setTotal(totalServicios);
                reservaDAO.actualizar(r);
            }
        } else {
            Reserva r = new Reserva(0, idCliente, idHabitacion, fechaEntrada, fechaSalida, estado);
            r.setServicios(listaServicios);
            r.setTotal(totalServicios);
            reservaDAO.agregar(r);
            int newId = r.getIdReserva();
            for (ReservaServicio rs : r.getServicios()) {
                rs.setIdReserva(newId);
            }
        }
        resp.sendRedirect(req.getContextPath() + "/reserva");
    }
}