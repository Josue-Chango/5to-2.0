package com.hotel.dao;

import com.hotel.model.Habitacion;
import com.hotel.util.DatabaseUtil;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class HabitacionDAO {

    public List<Habitacion> listar() {
        List<Habitacion> lista = new ArrayList<>();
        String sql = "SELECT * FROM habitacion ORDER BY id_habitacion";
        try (Connection conn = DatabaseUtil.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                lista.add(new Habitacion(
                        rs.getInt("id_habitacion"),
                        rs.getString("numero"),
                        rs.getInt("piso"),
                        rs.getString("estado"),
                        rs.getInt("id_tipo")
                ));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return lista;
    }

    public Habitacion buscar(int id) {
        String sql = "SELECT * FROM habitacion WHERE id_habitacion = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            try (ResultSet rs = ps.executeQuery()) {
                if (rs.next()) {
                    return new Habitacion(
                            rs.getInt("id_habitacion"),
                            rs.getString("numero"),
                            rs.getInt("piso"),
                            rs.getString("estado"),
                            rs.getInt("id_tipo")
                    );
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void agregar(Habitacion habitacion) {
        String sql = "INSERT INTO habitacion (numero, piso, estado, id_tipo) VALUES (?, ?, ?, ?)";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            ps.setString(1, habitacion.getNumero());
            ps.setInt(2, habitacion.getPiso());
            ps.setString(3, habitacion.getEstado());
            ps.setInt(4, habitacion.getIdTipo());
            ps.executeUpdate();
            try (ResultSet rs = ps.getGeneratedKeys()) {
                if (rs.next()) {
                    habitacion.setIdHabitacion(rs.getInt(1));
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void actualizar(Habitacion habitacion) {
        String sql = "UPDATE habitacion SET numero = ?, piso = ?, estado = ?, id_tipo = ? WHERE id_habitacion = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setString(1, habitacion.getNumero());
            ps.setInt(2, habitacion.getPiso());
            ps.setString(3, habitacion.getEstado());
            ps.setInt(4, habitacion.getIdTipo());
            ps.setInt(5, habitacion.getIdHabitacion());
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void eliminar(int id) {
        String sql = "DELETE FROM habitacion WHERE id_habitacion = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
