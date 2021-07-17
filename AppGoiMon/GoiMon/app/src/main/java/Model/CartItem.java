package Model;

public class CartItem {
    private int maMon;
    private String tenMon;
    private String anh;
    private int soLuong;
    private double giaGoc;
    private double giaKM;
    private double thanhTien;

    public CartItem() {
    }

    public CartItem(int maMon, String tenMon, String anh, int soLuong, double giaGoc, double giaKM, double thanhTien) {
        this.maMon = maMon;
        this.tenMon = tenMon;
        this.anh = anh;
        this.soLuong = soLuong;
        this.giaGoc = giaGoc;
        this.giaKM = giaKM;
        this.thanhTien = thanhTien;
    }

    public CartItem(int maMon, String tenMon, String anh, double giaGoc, double giaKM) {
        this.maMon = maMon;
        this.tenMon = tenMon;
        this.anh = anh;
        this.giaGoc = giaGoc;
        this.giaKM = giaKM;
    }

    public CartItem(int maMon, String tenMon, String anh, int soLuong, double giaGoc, double giaKM) {
        this.maMon = maMon;
        this.tenMon = tenMon;
        this.anh = anh;
        this.soLuong = soLuong;
        this.giaGoc = giaGoc;
        this.giaKM = giaKM;
    }

    public int getMaMon() {
        return maMon;
    }

    public void setMaMon(int maMon) {
        this.maMon = maMon;
    }

    public String getTenMon() {
        return tenMon;
    }

    public void setTenMon(String tenMon) {
        this.tenMon = tenMon;
    }

    public String getAnh() {
        return anh;
    }

    public void setAnh(String anh) {
        this.anh = anh;
    }

    public int getSoLuong() {
        return soLuong;
    }

    public void setSoLuong(int soLuong) {
        this.soLuong = soLuong;
    }

    public double getGiaGoc() {
        return giaGoc;
    }

    public void setGiaGoc(double giaGoc) {
        this.giaGoc = giaGoc;
    }

    public double getGiaKM() {
        return giaKM;
    }

    public void setGiaKM(double giaKM) {
        this.giaKM = giaKM;
    }

    public double getThanhTien() {
        return this.soLuong * this.giaKM;
    }

    public void setThanhTien(double thanhTien) {
        this.thanhTien = thanhTien;
    }

    @Override
    public String toString() {
        return "CartItem{" +
                "maMon=" + maMon +
                ", tenMon='" + tenMon + '\'' +
                ", anh='" + anh + '\'' +
                ", soLuong=" + soLuong +
                ", giaGoc=" + giaGoc +
                ", giaKM=" + giaKM +
                ", thanhTien=" + thanhTien +
                '}';
    }
}
