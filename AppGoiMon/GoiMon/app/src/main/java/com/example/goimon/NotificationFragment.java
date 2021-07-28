package com.example.goimon;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import java.io.Serializable;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

import Adapter.NotificationAdapter;
import Model.Cart;
import Model.CartItem;
import Model.History;
import Model.Mon;

public class NotificationFragment extends Fragment {
     TextView txtMaBan;
     ConnectionHelper connectionHelper = new ConnectionHelper();
     Connection connection;
     Cart cart;
     History history;
     ArrayList<CartItem> monArrayList;
     RecyclerView recyclerNotification;
     NotificationAdapter notificationAdapter;
     int maHD;


    public NotificationFragment() {
        // Required empty public constructor
    }



    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_notification, container, false);

    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        //
        recyclerNotification = view.findViewById(R.id.recyclerNotification);
        maHD = getMaHDByMB(getPreferences("maBan"));
        if(maHD == 0){
            return;
        }

        //Log.d("KRT","MaHD"+getMonAnByMaHD(getMaHDByMB(getPreferences("maBan"))));
        //monArrayList = getMonAnByMaHD(getMaHDByMB(getPreferences("maBan")));
        //notificationAdapter = new NotificationAdapter(getContext(),getMonAnByMaHD(maHD));
        notificationAdapter = new NotificationAdapter(getContext(),history.getInstanceHistory());
        recyclerNotification.setAdapter(notificationAdapter);
        recyclerNotification.setLayoutManager(new LinearLayoutManager(getContext(),RecyclerView.VERTICAL,false));//Vy vieidti
    }
    private ArrayList<CartItem> getMonAnByMaHD(int maHoaDon){
        connection = connectionHelper.getConnection();
        ArrayList<CartItem> arrLstMon = new ArrayList<>();
        if(connection != null){
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("Select Mon.MaMon, Mon.TenMon, Mon.GiaKM, Mon.GiaGoc, Mon.Anh, GoiMonTaiBan.SoLuong from Mon, GoiMonTaiBan where Mon.MaMon = GoiMonTaiBan.MaMon and GoiMonTaiBan.MaHD='"+maHoaDon+"'");
                if(resultSet != null){
                   while (resultSet.next()){
                       int maMon = resultSet.getInt(1);
                       String tenMon = resultSet.getString(2);
                       double giaKM = resultSet.getDouble(3);
                       double giaGoc = resultSet.getDouble(4);
                       String anh = resultSet.getString(5);
                       int soLuong = resultSet.getInt(6);
                       arrLstMon.add(new CartItem(maMon,tenMon,anh,soLuong,giaGoc,giaKM));
                   }
                }

            } catch (SQLException throwables) {
                throwables.printStackTrace();
            }
        }
        try {
            connection.close();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return arrLstMon;
    }
    public int getPreferences(String key){
        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("caches", Context.MODE_PRIVATE);
        return sharedPreferences.getInt(key,0);
    }
    public int getMaHDByMB(int maBan){
        connection = connectionHelper.getConnection();
        if(connection != null){
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("select MaHD from HoaDon where MaBan = '"+maBan+"' and TinhTrang = 0");
                if(resultSet != null){
                    if(!resultSet.next()){
                        Log.d("KRT","GetMaHDByMaBan: MaBan: " + maBan + " ,Khong tim thay MaHD");
                        return -1;
                    }
                    else{
                        Log.d("KRT","GetMaHDByMaBan: MaBan: " + maBan + " ,MaHD: " + resultSet.getInt(1));
                        return resultSet.getInt(1);
                    }
                }
                else
                {
                    Log.d("KRT","ResultSet is null");
                }
            } catch (SQLException throwables) {
                throwables.printStackTrace();
            }
        }
        else {
            Toast.makeText(getContext(), "Connect is null", Toast.LENGTH_SHORT).show();
        }
        try {
            connection.close();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return -1;
    }

    public  void updateHistory(ArrayList<CartItem> arr){

    }
}