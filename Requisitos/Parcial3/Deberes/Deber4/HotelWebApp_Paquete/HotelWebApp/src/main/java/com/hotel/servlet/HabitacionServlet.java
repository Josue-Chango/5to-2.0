package com.hotel.servlet;

import com.hotel.dao.HabitacionDAO;
import com.hotel.dao.TipoHabitacionDAO;
import com.hotel.model.Habitacion;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet("/habitacion")
public class HabitacionServlet extends HttpServlet {

    private HabitacionDAO habitacionDAO = new HabitacionDAO();
    private TipoHabitacionDAO tipoDAO = new TipoHabitacionDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null || action.equals("listar")) {
            req.setAttribute("habitaciones", habitacionDAO.listar());
            req.setAttribute("tipos", tipoDAO.listar());
            req.getRequestDispatcher("/habitacion/listar.jsp").forward(req, resp);
        } else if (action.equals("nuevo")) {
            req.setAttribute("tipos", tipoDAO.listar());
            req.getRequestDispatcher("/habitacion/form.jsp").forward(req, resp);
        } else if (action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Habitacion h = habitacionDAO.buscar(id);
            req.setAttribute("habitacion", h);
            req.setAttribute("tipos", tipoDAO.listar());
            req.getRequestDispatcher("/habitacion/form.jsp").forward(req, resp);
        } else if (action.equals("eliminar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            habitacionDAO.eliminar(id);
            resp.sendRedirect(req.getContextPath() + "/habitacion");
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        String numero = req.getParameter("numero");
        int piso = Integer.parseInt(req.getParameter("piso"));
        String estado = req.getParameter("estado");
        int idTipo = Integer.parseInt(req.getParameter("idTipo"));

        if (action != null && action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Habitacion h = new Habitacion(id, numero, piso, estado, idTipo);
            habitacionDAO.actualizar(h);
        } else {
            Habitacion h = new Habitacion(0, numero, piso, estado, idTipo);
            habitacionDAO.agregar(h);
        }
        resp.sendRedirect(req.getContextPath() + "/habitacion");
    }
}