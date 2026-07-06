package com.hotel.dao;

import com.hotel.model.Gasto;
import com.hotel.util.DatabaseUtil;
import java.sql.*;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class GastoDAO {

    public List<Gasto> listar() {
        List<Gasto> lista = new ArrayList<>();
        String sql = "SELECT * FROM gasto ORDER BY id_gasto";
        try (Connection conn = DatabaseUtil.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                lista.add(mapGasto(rs));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return lista;
    }

    public List<Gasto> listarPorReserva(int idReserva) {
        List<Gasto> lista = new ArrayList<>();
        String sql = "SELECT * FROM gasto WHERE id_reserva = ? ORDER BY id_gasto";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, idReserva);
            try (ResultSet rs = ps.executeQuery()) {
                while (rs.next()) {
                    lista.add(mapGasto(rs));
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return lista;
    }

    public Gasto buscar(int id) {
        String sql = "SELECT * FROM gasto WHERE id_gasto = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            try (ResultSet rs = ps.executeQuery()) {
                if (rs.next()) {
                    return mapGasto(rs);
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void agregar(Gasto gasto) {
        String sql = "INSERT INTO gasto (id_reserva, descripcion, monto, fecha) VALUES (?, ?, ?, ?)";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            ps.setInt(1, gasto.getIdReserva());
            ps.setString(2, gasto.getDescripcion());
            ps.setDouble(3, gasto.getMonto());
            ps.setDate(4, Date.valueOf(gasto.getFecha()));
            ps.executeUpdate();
            try (ResultSet rs = ps.getGeneratedKeys()) {
                if (rs.next()) {
                    gasto.setIdGasto(rs.getInt(1));
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void actualizar(Gasto gasto) {
        String sql = "UPDATE gasto SET id_reserva = ?, descripcion = ?, monto = ?, fecha = ? WHERE id_gasto = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, gasto.getIdReserva());
            ps.setString(2, gasto.getDescripcion());
            ps.setDouble(3, gasto.getMonto());
            ps.setDate(4, Date.valueOf(gasto.getFecha()));
            ps.setInt(5, gasto.getIdGasto());
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void eliminar(int id) {
        String sql = "DELETE FROM gasto WHERE id_gasto = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private Gasto mapGasto(ResultSet rs) throws SQLException {
        Date sqlDate = rs.getDate("fecha");
        LocalDate fecha = sqlDate != null ? sqlDate.toLocalDate() : null;
        return new Gasto(
                rs.getInt("id_gasto"),
                rs.getInt("id_reserva"),
                rs.getString("descripcion"),
                rs.getDouble("monto"),
                fecha
        );
    }
}
