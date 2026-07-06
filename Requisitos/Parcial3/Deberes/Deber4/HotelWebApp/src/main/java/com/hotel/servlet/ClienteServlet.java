package com.hotel.servlet;

import com.hotel.dao.ClienteDAO;
import com.hotel.model.Cliente;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet("/cliente")
public class ClienteServlet extends HttpServlet {

    private ClienteDAO clienteDAO = new ClienteDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null || action.equals("listar")) {
            req.setAttribute("clientes", clienteDAO.listar());
            req.getRequestDispatcher("/cliente/listar.jsp").forward(req, resp);
        } else if (action.equals("nuevo")) {
            req.getRequestDispatcher("/cliente/form.jsp").forward(req, resp);
        } else if (action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Cliente c = clienteDAO.buscar(id);
            req.setAttribute("cliente", c);
            req.getRequestDispatcher("/cliente/form.jsp").forward(req, resp);
        } else if (action.equals("eliminar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            clienteDAO.eliminar(id);
            resp.sendRedirect(req.getContextPath() + "/cliente");
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        String nombre = req.getParameter("nombre");
        String apellido = req.getParameter("apellido");
        String dni = req.getParameter("dni");
        String telefono = req.getParameter("telefono");
        String email = req.getParameter("email");
        String direccion = req.getParameter("direccion");

        if (action != null && action.equals("editar")) {
            int id = Integer.parseInt(req.getParameter("id"));
            Cliente c = new Cliente(id, nombre, apellido, dni, telefono, email, direccion);
            clienteDAO.actualizar(c);
        } else {
            Cliente c = new Cliente(0, nombre, apellido, dni, telefono, email, direccion);
            clienteDAO.agregar(c);
        }
        resp.sendRedirect(req.getContextPath() + "/cliente");
    }
}