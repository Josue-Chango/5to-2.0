package com.hotel.servlet;

import com.hotel.dao.ServicioDAO;
import com.hotel.model.Servicio;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet("/servicio")
public class ServicioServlet extends HttpServlet {

    private ServicioDAO servicioDAO = new ServicioDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null || action.equals("listar")) {
            req.setAttribute("servicios", servicioDAO.listar());
            req.getRequestDispatcher("/servicio/listar.jsp").forward(req, resp);
        } else if (action.equals("nuevo")) {
            req.getRequestDispatcher("/servicio/form.jsp").forward(req, resp);
        } else if (action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Servicio s = servicioDAO.buscar(id);
            req.setAttribute("servicio", s);
            req.getRequestDispatcher("/servicio/form.jsp").forward(req, resp);
        } else if (action.equals("eliminar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            servicioDAO.eliminar(id);
            resp.sendRedirect(req.getContextPath() + "/servicio");
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        String nombre = req.getParameter("nombre");
        String descripcion = req.getParameter("descripcion");
        double precio = Double.parseDouble(req.getParameter("precio"));

        if (action != null && action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Servicio s = new Servicio(id, nombre, descripcion, precio);
            servicioDAO.actualizar(s);
        } else {
            Servicio s = new Servicio(0, nombre, descripcion, precio);
            servicioDAO.agregar(s);
        }
        resp.sendRedirect(req.getContextPath() + "/servicio");
    }
}