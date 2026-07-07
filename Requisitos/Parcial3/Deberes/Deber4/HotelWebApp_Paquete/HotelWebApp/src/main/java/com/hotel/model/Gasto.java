package com.hotel.model;

import java.time.LocalDate;

public class Gasto {

    private int idGasto;
    private int idReserva;
    private String descripcion;
    private double monto;
    private LocalDate fecha;

    public Gasto() {
    }

    public Gasto(int idGasto, int idReserva, String descripcion, double monto, LocalDate fecha) {
        this.idGasto = idGasto;
        this.idReserva = idReserva;
        this.descripcion = descripcion;
        this.monto = monto;
        this.fecha = fecha;
    }

    public int getIdGasto() {
        return idGasto;
    }

    public void setIdGasto(int idGasto) {
        this.idGasto = idGasto;
    }

    public int getIdReserva() {
        return idReserva;
    }

    public void setIdReserva(int idReserva) {
        this.idReserva = idReserva;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public double getMonto() {
        return monto;
    }

    public void setMonto(double monto) {
        this.monto = monto;
    }

    public LocalDate getFecha() {
        return fecha;
    }

    public void setFecha(LocalDate fecha) {
        this.fecha = fecha;
    }
}
