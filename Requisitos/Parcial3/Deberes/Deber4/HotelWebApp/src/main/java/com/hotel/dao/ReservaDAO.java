package com.hotel.dao;

import com.hotel.model.Reserva;
import com.hotel.model.ReservaServicio;
import com.hotel.util.DatabaseUtil;
import java.sql.*;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class ReservaDAO {

    public List<Reserva> listar() {
        List<Reserva> lista = new ArrayList<>();
        String sql = "SELECT * FROM reserva ORDER BY id_reserva";
        try (Connection conn = DatabaseUtil.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                Reserva r = mapReserva(rs);
                r.setServicios(cargarServicios(conn, r.getIdReserva()));
                lista.add(r);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return lista;
    }

    public Reserva buscar(int id) {
        String sql = "SELECT * FROM reserva WHERE id_reserva = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            try (ResultSet rs = ps.executeQuery()) {
                if (rs.next()) {
                    Reserva r = mapReserva(rs);
                    r.setServicios(cargarServicios(conn, r.getIdReserva()));
                    return r;
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void agregar(Reserva reserva) {
        String sql = "INSERT INTO reserva (id_cliente, id_habitacion, fecha_entrada, fecha_salida, estado, fecha_reserva, total) " +
                "VALUES (?, ?, ?, ?, ?, ?, ?)";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            ps.setInt(1, reserva.getIdCliente());
            ps.setInt(2, reserva.getIdHabitacion());
            ps.setDate(3, Date.valueOf(reserva.getFechaEntrada()));
            ps.setDate(4, Date.valueOf(reserva.getFechaSalida()));
            ps.setString(5, reserva.getEstado());
            ps.setDate(6, Date.valueOf(reserva.getFechaReserva()));
            ps.setDouble(7, reserva.getTotal());
            ps.executeUpdate();
            try (ResultSet rs = ps.getGeneratedKeys()) {
                if (rs.next()) {
                    reserva.setIdReserva(rs.getInt(1));
                }
            }
            guardarServicios(conn, reserva.getIdReserva(), reserva.getServicios());
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void actualizar(Reserva reserva) {
        String sql = "UPDATE reserva SET id_cliente = ?, id_habitacion = ?, fecha_entrada = ?, fecha_salida = ?, " +
                "estado = ?, total = ? WHERE id_reserva = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, reserva.getIdCliente());
            ps.setInt(2, reserva.getIdHabitacion());
            ps.setDate(3, Date.valueOf(reserva.getFechaEntrada()));
            ps.setDate(4, Date.valueOf(reserva.getFechaSalida()));
            ps.setString(5, reserva.getEstado());
            ps.setDouble(6, reserva.getTotal());
            ps.setInt(7, reserva.getIdReserva());
            ps.executeUpdate();
            eliminarServicios(conn, reserva.getIdReserva());
            guardarServicios(conn, reserva.getIdReserva(), reserva.getServicios());
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void eliminar(int id) {
        try (Connection conn = DatabaseUtil.getConnection()) {
            eliminarServicios(conn, id);
            PreparedStatement ps = conn.prepareStatement("DELETE FROM gasto WHERE id_reserva = ?");
            ps.setInt(1, id);
            ps.executeUpdate();
            ps.close();
            ps = conn.prepareStatement("DELETE FROM reserva WHERE id_reserva = ?");
            ps.setInt(1, id);
            ps.executeUpdate();
            ps.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private List<ReservaServicio> cargarServicios(Connection conn, int idReserva) throws SQLException {
        List<ReservaServicio> lista = new ArrayList<>();
        String sql = "SELECT * FROM reserva_servicio WHERE id_reserva = ?";
        try (PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, idReserva);
            try (ResultSet rs = ps.executeQuery()) {
                while (rs.next()) {
                    lista.add(new ReservaServicio(
                            rs.getInt("id_reserva"),
                            rs.getInt("id_servicio"),
                            rs.getInt("cantidad"),
                            rs.getDouble("subtotal")
                    ));
                }
            }
        }
        return lista;
    }

    private void guardarServicios(Connection conn, int idReserva, List<ReservaServicio> servicios) throws SQLException {
        if (servicios == null) return;
        String sql = "INSERT INTO reserva_servicio (id_reserva, id_servicio, cantidad, subtotal) VALUES (?, ?, ?, ?)";
        try (PreparedStatement ps = conn.prepareStatement(sql)) {
            for (ReservaServicio rs : servicios) {
                ps.setInt(1, idReserva);
                ps.setInt(2, rs.getIdServicio());
                ps.setInt(3, rs.getCantidad());
                ps.setDouble(4, rs.getSubtotal());
                ps.executeUpdate();
            }
        }
    }

    private void eliminarServicios(Connection conn, int idReserva) throws SQLException {
        try (PreparedStatement ps = conn.prepareStatement("DELETE FROM reserva_servicio WHERE id_reserva = ?")) {
            ps.setInt(1, idReserva);
            ps.executeUpdate();
        }
    }

    private Reserva mapReserva(ResultSet rs) throws SQLException {
        Reserva r = new Reserva();
        r.setIdReserva(rs.getInt("id_reserva"));
        r.setIdCliente(rs.getInt("id_cliente"));
        r.setIdHabitacion(rs.getInt("id_habitacion"));
        Date fechaEnt = rs.getDate("fecha_entrada");
        if (fechaEnt != null) r.setFechaEntrada(fechaEnt.toLocalDate());
        Date fechaSal = rs.getDate("fecha_salida");
        if (fechaSal != null) r.setFechaSalida(fechaSal.toLocalDate());
        r.setEstado(rs.getString("estado"));
        Date fechaRes = rs.getDate("fecha_reserva");
        if (fechaRes != null) r.setFechaReserva(fechaRes.toLocalDate());
        r.setTotal(rs.getDouble("total"));
        r.setServicios(new ArrayList<>());
        return r;
    }
}
