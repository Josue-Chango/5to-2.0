package com.hotel.model;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class Reserva {

    private int idReserva;
    private int idCliente;
    private int idHabitacion;
    private LocalDate fechaEntrada;
    private LocalDate fechaSalida;
    private String estado;
    private LocalDate fechaReserva;
    private double total;
    private List<ReservaServicio> servicios;

    public Reserva() {
        this.servicios = new ArrayList<>();
        this.fechaReserva = LocalDate.now();
    }

    public Reserva(int idReserva, int idCliente, int idHabitacion, LocalDate fechaEntrada, LocalDate fechaSalida, String estado) {
        this.idReserva = idReserva;
        this.idCliente = idCliente;
        this.idHabitacion = idHabitacion;
        this.fechaEntrada = fechaEntrada;
        this.fechaSalida = fechaSalida;
        this.estado = estado;
        this.fechaReserva = LocalDate.now();
        this.total = 0.0;
        this.servicios = new ArrayList<>();
    }

    public int getIdReserva() {
        return idReserva;
    }

    public void setIdReserva(int idReserva) {
        this.idReserva = idReserva;
    }

    public int getIdCliente() {
        return idCliente;
    }

    public void setIdCliente(int idCliente) {
        this.idCliente = idCliente;
    }

    public int getIdHabitacion() {
        return idHabitacion;
    }

    public void setIdHabitacion(int idHabitacion) {
        this.idHabitacion = idHabitacion;
    }

    public LocalDate getFechaEntrada() {
        return fechaEntrada;
    }

    public void setFechaEntrada(LocalDate fechaEntrada) {
        this.fechaEntrada = fechaEntrada;
    }

    public LocalDate getFechaSalida() {
        return fechaSalida;
    }

    public void setFechaSalida(LocalDate fechaSalida) {
        this.fechaSalida = fechaSalida;
    }

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public LocalDate getFechaReserva() {
        return fechaReserva;
    }

    public void setFechaReserva(LocalDate fechaReserva) {
        this.fechaReserva = fechaReserva;
    }

    public double getTotal() {
        return total;
    }

    public void setTotal(double total) {
        this.total = total;
    }

    public List<ReservaServicio> getServicios() {
        return servicios;
    }

    public void setServicios(List<ReservaServicio> servicios) {
        this.servicios = servicios;
    }

    public boolean hasServicio(int idServicio) {
        return servicios.stream().anyMatch(rs -> rs.getIdServicio() == idServicio);
    }
}
