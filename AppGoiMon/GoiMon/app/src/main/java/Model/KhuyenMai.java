package Model;

public class KhuyenMai {
    private int maKM;
    private String tenKM;
    private double tyLe;

    public KhuyenMai(int maKM, String tenKM, double tyLe) {
        this.maKM = maKM;
        this.tenKM = tenKM;
        this.tyLe = tyLe;
    }

    public int getMaKM() {
        return maKM;
    }

    public void setMaKM(int maKM) {
        this.maKM = maKM;
    }

    public String getTenKM() {
        return tenKM;
    }

    public void setTenKM(String tenKM) {
        this.tenKM = tenKM;
    }

    public double getTyLe() {
        return tyLe;
    }

    public void setTyLe(double tyLe) {
        this.tyLe = tyLe;
    }
}
