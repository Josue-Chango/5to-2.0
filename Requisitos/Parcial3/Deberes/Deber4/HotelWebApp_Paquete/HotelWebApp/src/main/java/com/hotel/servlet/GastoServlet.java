package com.hotel.servlet;

import com.hotel.dao.GastoDAO;
import com.hotel.dao.ReservaDAO;
import com.hotel.model.Gasto;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.time.LocalDate;

@WebServlet("/gasto")
public class GastoServlet extends HttpServlet {

    private GastoDAO gastoDAO = new GastoDAO();
    private ReservaDAO reservaDAO = new ReservaDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null || action.equals("listar")) {
            req.setAttribute("gastos", gastoDAO.listar());
            req.setAttribute("reservas", reservaDAO.listar());
            req.getRequestDispatcher("/gasto/listar.jsp").forward(req, resp);
        } else if (action.equals("nuevo")) {
            req.setAttribute("reservas", reservaDAO.listar());
            req.getRequestDispatcher("/gasto/form.jsp").forward(req, resp);
        } else if (action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Gasto g = gastoDAO.buscar(id);
            req.setAttribute("gasto", g);
            req.setAttribute("reservas", reservaDAO.listar());
            req.getRequestDispatcher("/gasto/form.jsp").forward(req, resp);
        } else if (action.equals("eliminar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            gastoDAO.eliminar(id);
            resp.sendRedirect(req.getContextPath() + "/gasto");
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        int idReserva = Integer.parseInt(req.getParameter("idReserva"));
        String descripcion = req.getParameter("descripcion");
        double monto = Double.parseDouble(req.getParameter("monto"));
        LocalDate fecha = LocalDate.parse(req.getParameter("fecha"));

        if (action != null && action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Gasto g = new Gasto(id, idReserva, descripcion, monto, fecha);
            gastoDAO.actualizar(g);
        } else {
            Gasto g = new Gasto(0, idReserva, descripcion, monto, fecha);
            gastoDAO.agregar(g);
        }
        resp.sendRedirect(req.getContextPath() + "/gasto");
    }
}