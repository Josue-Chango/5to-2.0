package com.hotel.util;

import java.sql.*;

public class DatabaseUtil {

    private static final String JDBC_URL = "jdbc:h2:~/HotelWebApp;AUTO_SERVER=TRUE";
    private static final String USER = "sa";
    private static final String PASSWORD = "";

    static {
        try {
            Class.forName("org.h2.Driver");
            initDatabase();
        } catch (ClassNotFoundException e) {
            throw new RuntimeException("H2 Driver not found", e);
        }
    }

    public static Connection getConnection() throws SQLException {
        return DriverManager.getConnection(JDBC_URL, USER, PASSWORD);
    }

    private static void initDatabase() {
        try (Connection conn = getConnection(); Statement stmt = conn.createStatement()) {

            stmt.execute("CREATE TABLE IF NOT EXISTS tipo_habitacion (" +
                    "id_tipo INT AUTO_INCREMENT PRIMARY KEY, " +
                    "nombre VARCHAR(100) NOT NULL, " +
                    "descripcion VARCHAR(255), " +
                    "capacidad INT NOT NULL, " +
                    "precio_base DOUBLE NOT NULL)");

            stmt.execute("CREATE TABLE IF NOT EXISTS habitacion (" +
                    "id_habitacion INT AUTO_INCREMENT PRIMARY KEY, " +
                    "numero VARCHAR(10) NOT NULL, " +
                    "piso INT NOT NULL, " +
                    "estado VARCHAR(20) NOT NULL, " +
                    "id_tipo INT, " +
                    "FOREIGN KEY (id_tipo) REFERENCES tipo_habitacion(id_tipo))");

            stmt.execute("CREATE TABLE IF NOT EXISTS cliente (" +
                    "id_cliente INT AUTO_INCREMENT PRIMARY KEY, " +
                    "nombre VARCHAR(100) NOT NULL, " +
                    "apellido VARCHAR(100) NOT NULL, " +
                    "dni VARCHAR(20) NOT NULL, " +
                    "telefono VARCHAR(20), " +
                    "email VARCHAR(100), " +
                    "direccion VARCHAR(255))");

            stmt.execute("CREATE TABLE IF NOT EXISTS servicio (" +
                    "id_servicio INT AUTO_INCREMENT PRIMARY KEY, " +
                    "nombre VARCHAR(100) NOT NULL, " +
                    "descripcion VARCHAR(255), " +
                    "precio DOUBLE NOT NULL)");

            stmt.execute("CREATE TABLE IF NOT EXISTS reserva (" +
                    "id_reserva INT AUTO_INCREMENT PRIMARY KEY, " +
                    "id_cliente INT NOT NULL, " +
                    "id_habitacion INT NOT NULL, " +
                    "fecha_entrada DATE NOT NULL, " +
                    "fecha_salida DATE NOT NULL, " +
                    "estado VARCHAR(20) NOT NULL, " +
                    "fecha_reserva DATE NOT NULL, " +
                    "total DOUBLE, " +
                    "FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente), " +
                    "FOREIGN KEY (id_habitacion) REFERENCES habitacion(id_habitacion))");

            stmt.execute("CREATE TABLE IF NOT EXISTS reserva_servicio (" +
                    "id_reserva INT NOT NULL, " +
                    "id_servicio INT NOT NULL, " +
                    "cantidad INT NOT NULL, " +
                    "subtotal DOUBLE NOT NULL, " +
                    "PRIMARY KEY (id_reserva, id_servicio), " +
                    "FOREIGN KEY (id_reserva) REFERENCES reserva(id_reserva), " +
                    "FOREIGN KEY (id_servicio) REFERENCES servicio(id_servicio))");

            stmt.execute("CREATE TABLE IF NOT EXISTS gasto (" +
                    "id_gasto INT AUTO_INCREMENT PRIMARY KEY, " +
                    "id_reserva INT NOT NULL, " +
                    "descripcion VARCHAR(255) NOT NULL, " +
                    "monto DOUBLE NOT NULL, " +
                    "fecha DATE NOT NULL, " +
                    "FOREIGN KEY (id_reserva) REFERENCES reserva(id_reserva))");

            ResultSet rs = stmt.executeQuery("SELECT COUNT(*) FROM tipo_habitacion");
            rs.next();
            if (rs.getInt(1) == 0) {
                stmt.executeUpdate("INSERT INTO tipo_habitacion (nombre, descripcion, capacidad, precio_base) " +
                        "VALUES ('Simple', 'Habitaci\u00f3n b\u00e1sica con cama individual', 1, 50.0)");
                stmt.executeUpdate("INSERT INTO tipo_habitacion (nombre, descripcion, capacidad, precio_base) " +
                        "VALUES ('Doble', 'Habitaci\u00f3n con dos camas individuales', 2, 80.0)");
                stmt.executeUpdate("INSERT INTO tipo_habitacion (nombre, descripcion, capacidad, precio_base) " +
                        "VALUES ('Suite', 'Habitaci\u00f3n de lujo con sala y cama king', 3, 150.0)");

                stmt.executeUpdate("INSERT INTO habitacion (numero, piso, estado, id_tipo) " +
                        "VALUES ('101', 1, 'Disponible', 1)");
                stmt.executeUpdate("INSERT INTO habitacion (numero, piso, estado, id_tipo) " +
                        "VALUES ('102', 1, 'Disponible', 1)");
                stmt.executeUpdate("INSERT INTO habitacion (numero, piso, estado, id_tipo) " +
                        "VALUES ('201', 2, 'Disponible', 2)");
                stmt.executeUpdate("INSERT INTO habitacion (numero, piso, estado, id_tipo) " +
                        "VALUES ('202', 2, 'Disponible', 2)");
                stmt.executeUpdate("INSERT INTO habitacion (numero, piso, estado, id_tipo) " +
                        "VALUES ('301', 3, 'Disponible', 3)");

                stmt.executeUpdate("INSERT INTO cliente (nombre, apellido, dni, telefono, email, direccion) " +
                        "VALUES ('Juan', 'P\u00e9rez', '12345678', '555-0101', 'juan@email.com', 'Calle A #123')");
                stmt.executeUpdate("INSERT INTO cliente (nombre, apellido, dni, telefono, email, direccion) " +
                        "VALUES ('Mar\u00eda', 'Garc\u00eda', '87654321', '555-0102', 'maria@email.com', 'Calle B #456')");

                stmt.executeUpdate("INSERT INTO servicio (nombre, descripcion, precio) " +
                        "VALUES ('Desayuno', 'Desayuno buffet continental', 15.0)");
                stmt.executeUpdate("INSERT INTO servicio (nombre, descripcion, precio) " +
                        "VALUES ('Lavander\u00eda', 'Servicio de lavado y planchado', 25.0)");
                stmt.executeUpdate("INSERT INTO servicio (nombre, descripcion, precio) " +
                        "VALUES ('Cena', 'Cena gourmet en el restaurante', 35.0)");
                stmt.executeUpdate("INSERT INTO servicio (nombre, descripcion, precio) " +
                        "VALUES ('Spa', 'Acceso al spa por un d\u00eda', 50.0)");
            }

        } catch (SQLException e) {
            throw new RuntimeException("Failed to initialize H2 database", e);
        }
    }
}
