package com.hotel.servlet;

import com.hotel.dao.TipoHabitacionDAO;
import com.hotel.model.TipoHabitacion;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet("/tipoHabitacion")
public class TipoHabitacionServlet extends HttpServlet {

    private TipoHabitacionDAO tipoDAO = new TipoHabitacionDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null || action.equals("listar")) {
            req.setAttribute("tipos", tipoDAO.listar());
            req.getRequestDispatcher("/tipoHabitacion/listar.jsp").forward(req, resp);
        } else if (action.equals("nuevo")) {
            req.getRequestDispatcher("/tipoHabitacion/form.jsp").forward(req, resp);
        } else if (action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            TipoHabitacion t = tipoDAO.buscar(id);
            req.setAttribute("tipo", t);
            req.getRequestDispatcher("/tipoHabitacion/form.jsp").forward(req, resp);
        } else if (action.equals("eliminar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            tipoDAO.eliminar(id);
            resp.sendRedirect(req.getContextPath() + "/tipoHabitacion");
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        String nombre = req.getParameter("nombre");
        String descripcion = req.getParameter("descripcion");
        int capacidad = Integer.parseInt(req.getParameter("capacidad"));
        double precioBase = Double.parseDouble(req.getParameter("precioBase"));

        if (action != null && action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            TipoHabitacion t = new TipoHabitacion(id, nombre, descripcion, capacidad, precioBase);
            tipoDAO.actualizar(t);
        } else {
            TipoHabitacion t = new TipoHabitacion(0, nombre, descripcion, capacidad, precioBase);
            tipoDAO.agregar(t);
        }
        resp.sendRedirect(req.getContextPath() + "/tipoHabitacion");
    }
}