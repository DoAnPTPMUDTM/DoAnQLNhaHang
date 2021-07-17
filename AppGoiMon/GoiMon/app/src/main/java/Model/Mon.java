package Model;

import android.content.Context;
import android.content.res.AssetManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

import java.io.IOException;
import java.io.InputStream;

public class Mon {
    private int maMon;
    private int maNhom;
    private int maDVT;
    private String tenMon;
    private String anhMon;
    private double giaGoc;
    private double giaKM;
    private int maKM;

    public Mon() {
    }
    public Mon(int maMon, String tenMon, String anhMon, double giaGoc, double giaKM) {
        this.maMon = maMon;
        this.tenMon = tenMon;
        this.anhMon = anhMon;
        this.giaGoc = giaGoc;
        this.giaKM = giaKM;
    }
    public Mon(int maMon, int maNhom, int maDVT, String tenMon, String anhMon, double giaGoc, double giaKM, int maKM) {
        this.maMon = maMon;
        this.maNhom = maNhom;
        this.maDVT = maDVT;
        this.tenMon = tenMon;
        this.anhMon = anhMon;
        this.giaGoc = giaGoc;
        this.giaKM = giaKM;
        this.maKM = maKM;
    }



    public int getMaMon() {
        return maMon;
    }

    public void setMaMon(int maMon) {
        this.maMon = maMon;
    }

    public int getMaNhom() {
        return maNhom;
    }

    public void setMaNhom(int maNhom) {
        this.maNhom = maNhom;
    }

    public int getMaDVT() {
        return maDVT;
    }

    public void setMaDVT(int maDVT) {
        this.maDVT = maDVT;
    }

    public String getTenMon() {
        return tenMon;
    }

    public void setTenMon(String tenMon) {
        this.tenMon = tenMon;
    }

    public String getAnhMon() {
        return anhMon;
    }

    public void setAnhMon(String anhMon) {
        this.anhMon = anhMon;
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

    public int getMaKM() {
        return maKM;
    }

    public void setMaKM(int maKM) {
        this.maKM = maKM;
    }
    //convert anh mon an
    public static Bitmap convertStringToBitmapFromAccess(Context context, String filename){
        AssetManager assetManager = context.getAssets();
        try{
            InputStream inputStream = assetManager.open(filename);
            Bitmap bitmap = BitmapFactory.decodeStream(inputStream);
            return bitmap;
        }catch (IOException e){
            e.printStackTrace();
        }
        return null;
    }
}
