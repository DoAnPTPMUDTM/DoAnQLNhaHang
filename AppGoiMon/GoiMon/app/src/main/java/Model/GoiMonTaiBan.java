package Model;

public class GoiMonTaiBan {
    private int maGoiMon;
    private int maHD;
    private int maMon;
    private int soLuong;
    private int tinhTrang;

    public GoiMonTaiBan() {
    }

    public GoiMonTaiBan(int maGoiMon, int maHD, int maMon, int soLuong, int tinhTrang) {
        this.maGoiMon = maGoiMon;
        this.maHD = maHD;
        this.maMon = maMon;
        this.soLuong = soLuong;
        this.tinhTrang = tinhTrang;
    }

    public GoiMonTaiBan(int maHD, int maMon, int soLuong, int tinhTrang) {
        this.maHD = maHD;
        this.maMon = maMon;
        this.soLuong = soLuong;
        this.tinhTrang = tinhTrang;
    }

    public int getMaGoiMon() {
        return maGoiMon;
    }

    public void setMaGoiMon(int maGoiMon) {
        this.maGoiMon = maGoiMon;
    }

    public int getMaHD() {
        return maHD;
    }

    public void setMaHD(int maHD) {
        this.maHD = maHD;
    }

    public int getMaMon() {
        return maMon;
    }

    public void setMaMon(int maMon) {
        this.maMon = maMon;
    }

    public int getSoLuong() {
        return soLuong;
    }

    public void setSoLuong(int soLuong) {
        this.soLuong = soLuong;
    }

    public int getTinhTrang() {
        return tinhTrang;
    }

    public void setTinhTrang(int tinhTrang) {
        this.tinhTrang = tinhTrang;
    }
}
