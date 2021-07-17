package Model;

import java.util.ArrayList;

public class HoaDon {
    private String maHD;
    private int maBan;
    private int tinhTrang;


    public HoaDon(String maHD, int maBan, int tinhTrang) {
        this.maHD = maHD;
        this.maBan = maBan;
        this.tinhTrang = tinhTrang;
    }

    public String getMaHD() {
        return maHD;
    }

    public void setMaHD(String maHD) {
        this.maHD = maHD;
    }

    public int getMaBan() {
        return maBan;
    }

    public void setMaBan(int maBan) {
        this.maBan = maBan;
    }

    public int getTinhTrang() {
        return tinhTrang;
    }

    public void setTinhTrang(int tinhTrang) {
        this.tinhTrang = tinhTrang;
    }
}
