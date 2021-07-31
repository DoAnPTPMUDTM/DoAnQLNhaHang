package com.example.goimon;

import androidx.appcompat.app.AppCompatActivity;

import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.text.Layout;
import android.text.SpannableString;
import android.text.style.AlignmentSpan;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.HorizontalScrollView;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.logging.LogRecord;

import Model.NguoiDung;

public class LoginActivity extends AppCompatActivity {
    Button btnDangNhap;
    EditText edtTenDangNhap, edtMatKhau;
    ConnectionHelper connectionHelper = new ConnectionHelper();
    Connection connection = null;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        linkControl();
        //
        connection = connectionHelper.getConnection();
        //
        btnDangNhap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String tenDN = edtTenDangNhap.getText().toString().trim();
                String matKhau =edtMatKhau.getText().toString().trim();
                if(tenDN.isEmpty()){
                    edtTenDangNhap.setError("Tên đăng nhập không được để trống");
                    return;
                }
                if(matKhau.isEmpty()){
                    edtMatKhau.setError("Mật khẩu không được để trống");
                    return;
                }
                if(login(tenDN,matKhau)){
                //if(loginAccount(tenDN,matKhau)!= null){
                    //Toast.makeText(LoginActivity.this, "Đăng nhập thành công", Toast.LENGTH_SHORT).show();
                    Intent intent = new Intent(LoginActivity.this, DSBanActivity.class);
                    startActivity(intent);
                    finish();
                }
                else {
                    //Toast.makeText(LoginActivity.this, "Đăng nhập thất bại", Toast.LENGTH_SHORT).show();
                    AlertDialog.Builder alert = new AlertDialog.Builder(LoginActivity.this);
                    SpannableString title = new SpannableString("Thông báo");
                    title.setSpan(new AlignmentSpan.Standard(Layout.Alignment.ALIGN_CENTER),0,title.length(),0);
                    alert.setTitle(title);
                    alert.setMessage("Sai tên đăng nhập hoặc mật khẩu");
                    alert.setNegativeButton("OK",null);
                    alert.show().getWindow().setLayout(700,500);
                }
            }
        });
    }
    public boolean login(String tenDN, String matKhau){
        if(connection != null){
            Statement statement = null;
            try{
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("SELECT COUNT(*) AS 'Count' FROM NguoiDung WHERE TenDN='"+tenDN+"' AND MatKhau='"+matKhau+"'");
                while (resultSet.next()) {
                    return resultSet.getInt(1) > 0;
                }
            }catch (Exception e){
                Toast.makeText(LoginActivity.this, ""+e.getMessage(), Toast.LENGTH_SHORT).show();
            }
        }
        return false;
    }
    private NguoiDung loginAccount(String tenDN, String matKhau){
        if(connection != null){
            Statement statement = null;
            try{
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("SELECT * FROM NguoiDung WHERE TenDN='"+tenDN+"' AND MatKhau='"+matKhau+"'");
                if(resultSet != null){
                    while (resultSet.next()){
                        int maND = resultSet.getInt(1);
                        //Log.d("Vy", String.valueOf(resultSet.getInt(1)));
                        String hoTen = resultSet.getString(2);
                        String gioiTinh = resultSet.getString(3);
                        String SDT = resultSet.getString(4);
                        String diaChi = resultSet.getString(5);
                        String Email = resultSet.getString(6);
                        String TenDN = resultSet.getString(7);
                        //Log.d("Vy",resultSet.getString(7));
                        String MatKhau = resultSet.getString(8);
                        String hoatDong = resultSet.getString(9);
                        return (new NguoiDung(maND,hoTen,gioiTinh,SDT,diaChi,Email,TenDN,MatKhau, hoatDong));
                    }
                }
                else{
                    Toast.makeText(LoginActivity.this, "Loi ket noi", Toast.LENGTH_SHORT).show();
                }
            }catch (Exception e){
                Toast.makeText(LoginActivity.this, ""+e.getMessage(), Toast.LENGTH_SHORT).show();
            }
        }
        return null;
    }

    private void linkControl() {
        edtTenDangNhap = findViewById(R.id.edtTenDangNhap);
        edtMatKhau = findViewById(R.id.edtMatKhau);
        btnDangNhap = findViewById(R.id.btnDangNhap);
    }
}