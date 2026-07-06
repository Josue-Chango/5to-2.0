package com.hotel.model;

public class Habitacion {

    private int idHabitacion;
    private String numero;
    private int piso;
    private String estado;
    private int idTipo;

    public Habitacion() {
    }

    public Habitacion(int idHabitacion, String numero, int piso, String estado, int idTipo) {
        this.idHabitacion = idHabitacion;
        this.numero = numero;
        this.piso = piso;
        this.estado = estado;
        this.idTipo = idTipo;
    }

    public int getIdHabitacion() {
        return idHabitacion;
    }

    public void setIdHabitacion(int idHabitacion) {
        this.idHabitacion = idHabitacion;
    }

    public String getNumero() {
        return numero;
    }

    public void setNumero(String numero) {
        this.numero = numero;
    }

    public int getPiso() {
        return piso;
    }

    public void setPiso(int piso) {
        this.piso = piso;
    }

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public int getIdTipo() {
        return idTipo;
    }

    public void setIdTipo(int idTipo) {
        this.idTipo = idTipo;
    }
}
