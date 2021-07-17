package com.example.goimon;

import androidx.appcompat.app.AppCompatActivity;

import android.app.FragmentTransaction;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.GridView;
import android.widget.ListView;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

import Adapter.BanAdapter;
import Model.Ban;
import Model.HoaDon;

public class DSBanActivity extends AppCompatActivity {
    //ListView lstDSBan;
    GridView gridDSBan;
    BanAdapter banAdapter;
    ArrayList<Ban> arrayList;
    ConnectionHelper connectionHelper = new ConnectionHelper();
    Connection connection;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_d_s_ban);
        //lstDSBan = findViewById(R.id.lstDSBan);
        gridDSBan = findViewById(R.id.gridDSBan);
        //
        connection = connectionHelper.getConnection();
        //
        arrayList = getDSBan();

        banAdapter = new BanAdapter(this, arrayList);
        gridDSBan.setAdapter(banAdapter);
        gridDSBan.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                //sau khi click xong thì save Share Pre.
//                savePreferences("maBan",arrayList.get(position).getMaBan());
//                savePreferences("tinhTrang",arrayList.get(position).getTrangThai());
//                savePreferences("maHD",Integer.parseInt(getMaHDByMaBan(arrayList.get(position).getMaBan()).getMaHD()));
//                Intent intent = new Intent(DSBanActivity.this, Main2Activity.class);
//                startActivity(intent);

                //if (getMaHDByMaBan(arrayList.get(position).getMaBan()).getMaHD() == null) {
                    //Log.d("KRT","null");
                    //return;
                //}
                //Log.d("KRT", "Mã bàn" + arrayList.get(position).getMaBan() + "Mã hd" + getMaHDByMaBan(arrayList.get(position).getMaBan()).getMaHD());

                //save sp maBan
                savePreferences("maBan",arrayList.get(position).getMaBan());
                Intent intent = new Intent(DSBanActivity.this, Main2Activity.class);
                startActivity(intent);
                finish();
            }
        });
    }

    private ArrayList<Ban> getDSBan() {
        arrayList = new ArrayList<>();
        if (connection != null) {
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("SELECT * FROM Ban");
                if (resultSet != null) {
                    while (resultSet.next()) {
                        int maBan = resultSet.getInt(1);
                        String tenBan = resultSet.getString(2);
                        int trangThai = resultSet.getInt(4);
                        arrayList.add(new Ban(maBan, tenBan, trangThai));
                    }
                }

            } catch (SQLException throwables) {
                throwables.printStackTrace();
            }
        } else {
            Toast.makeText(this, "Connection is null", Toast.LENGTH_SHORT).show();
        }
        try {
            connection.close();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return arrayList;
    }

    //    public boolean updateTrangThaiBan(int maBan)
//    {
//        connection = connectionHelper.getConnection();
//        if(connection != null){
//            Statement statement = null;
//            try {
//                statement = connection.createStatement();
//                String query = "Update Ban Set TrangThai = 1 Where MaBan='"+maBan+"'";
//
//                if(statement.executeUpdate(query) > 0){
//                    connection.close();
//                    return true;
//                }
//            } catch (SQLException throwables) {
//                throwables.printStackTrace();
//            }
//        }
//        return false;
//    }
    private HoaDon getMaHDByMaBan(int maBan) {
        connection = connectionHelper.getConnection();
        if (connection != null) {
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("select * from HoaDon where MaBan = '" + maBan + "' and TinhTrang = 0");
                if (resultSet != null) {
                    while (resultSet.next()) {
                        int maHD = resultSet.getInt(1);
                        int MaBan = resultSet.getInt(2);
                        int tinhTrangHD = resultSet.getInt(11);
                        return (new HoaDon(String.valueOf(maHD), MaBan, tinhTrangHD));
//                    int MaBan = resultSet.getInt(2);
//                    int tinhTrangHD = resultSet.getInt(11);
//                    return (new HoaDon(String.valueOf(maHD),MaBan,tinhTrangHD));
                    }
                } else {
                    Log.d("KRT", "Connect is null");
                }
            } catch (SQLException throwables) {
                throwables.printStackTrace();
            }
        } else {
            Toast.makeText(this, "Connect is null", Toast.LENGTH_SHORT).show();
        }
        try {
            connection.close();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return null;
    }




    public void savePreferences(String key, int value) {
        SharedPreferences sharedPreferences = getSharedPreferences("caches", Context.MODE_PRIVATE);
        SharedPreferences.Editor edCaches = sharedPreferences.edit();
        edCaches.putInt(key, value);
        edCaches.commit();
    }

    public void clearPreferences() {
        SharedPreferences p = getSharedPreferences("caches", Context.MODE_PRIVATE);
        SharedPreferences.Editor edCaches = p.edit();
        edCaches.clear();
        edCaches.commit();
    }

    //
    private Ban findHoaDonByMaBan(int maBan) {
        ArrayList<Ban> banArrayList = getDSBan();
        for (Ban ban : banArrayList) {
            if (ban.getMaBan() == maBan) {
                return ban;
            }
        }
        return null;
    }
}