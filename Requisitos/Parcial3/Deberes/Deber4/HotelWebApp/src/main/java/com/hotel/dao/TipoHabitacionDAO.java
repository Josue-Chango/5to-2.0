package com.hotel.dao;

import com.hotel.model.TipoHabitacion;
import com.hotel.util.DatabaseUtil;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class TipoHabitacionDAO {

    public List<TipoHabitacion> listar() {
        List<TipoHabitacion> lista = new ArrayList<>();
        String sql = "SELECT * FROM tipo_habitacion ORDER BY id_tipo";
        try (Connection conn = DatabaseUtil.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                lista.add(new TipoHabitacion(
                        rs.getInt("id_tipo"),
                        rs.getString("nombre"),
                        rs.getString("descripcion"),
                        rs.getInt("capacidad"),
                        rs.getDouble("precio_base")
                ));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return lista;
    }

    public TipoHabitacion buscar(int id) {
        String sql = "SELECT * FROM tipo_habitacion WHERE id_tipo = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            try (ResultSet rs = ps.executeQuery()) {
                if (rs.next()) {
                    return new TipoHabitacion(
                            rs.getInt("id_tipo"),
                            rs.getString("nombre"),
                            rs.getString("descripcion"),
                            rs.getInt("capacidad"),
                            rs.getDouble("precio_base")
                    );
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void agregar(TipoHabitacion tipo) {
        String sql = "INSERT INTO tipo_habitacion (nombre, descripcion, capacidad, precio_base) VALUES (?, ?, ?, ?)";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            ps.setString(1, tipo.getNombre());
            ps.setString(2, tipo.getDescripcion());
            ps.setInt(3, tipo.getCapacidad());
            ps.setDouble(4, tipo.getPrecioBase());
            ps.executeUpdate();
            try (ResultSet rs = ps.getGeneratedKeys()) {
                if (rs.next()) {
                    tipo.setIdTipo(rs.getInt(1));
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void actualizar(TipoHabitacion tipo) {
        String sql = "UPDATE tipo_habitacion SET nombre = ?, descripcion = ?, capacidad = ?, precio_base = ? WHERE id_tipo = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setString(1, tipo.getNombre());
            ps.setString(2, tipo.getDescripcion());
            ps.setInt(3, tipo.getCapacidad());
            ps.setDouble(4, tipo.getPrecioBase());
            ps.setInt(5, tipo.getIdTipo());
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void eliminar(int id) {
        String sql = "DELETE FROM tipo_habitacion WHERE id_tipo = ?";
        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, id);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
