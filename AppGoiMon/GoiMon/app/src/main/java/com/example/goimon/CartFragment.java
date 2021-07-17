package com.example.goimon;

import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
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
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Locale;

import Adapter.CartMonAdapter;
import Model.Ban;
import Model.Cart;
import Model.CartItem;
import Model.GoiMonTaiBan;
import Model.HoaDon;
import Model.OnCartCallBack;

public class CartFragment extends Fragment {
    RecyclerView recyclerCart;
    ConnectionHelper connectionHelper = new ConnectionHelper();
    Connection connection=null;
    CartMonAdapter cartMonAdapter;
    ArrayList<CartItem> cartItemArrayList;
    Cart cart;
    Button btnGoiMon;
    TextView txtThanhTienCart;
    Ban ban;

    public CartFragment() {
        // Required empty public constructor
    }

    // TODO: Rename and change types and number of parameters


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_cart, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        recyclerCart = view.findViewById(R.id.recyclerCart);
        txtThanhTienCart = view.findViewById(R.id.txtThanhTienCart);
        btnGoiMon = view.findViewById(R.id.btnGoiMon);
        //Bundle bundle = getArguments();

        //cartItemArrayList = new ArrayList<>();
        cartMonAdapter = new CartMonAdapter(getContext(), cart.getInstanceCart(), new OnCartCallBack() {
            @Override
            public void OnButtonCartCallBack() {
                //
                tinhThanhTien();
            }

            @Override
            public void OnButtonDeleteCartCallBack() {
                tinhThanhTien();
                if(cart.getInstanceCart().size() == 0){
                    Main2Activity.hideNumberCart();
                }else{
                    Main2Activity.showNumberCart(cart.getInstanceCart().size());
                }
            }
        });
        recyclerCart.setAdapter(cartMonAdapter);
        recyclerCart.setLayoutManager(new LinearLayoutManager(getContext(),RecyclerView.VERTICAL,false));
        tinhThanhTien();
        btnGoiMon.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(cart.getInstanceCart().size() == 0){
                    //Show tb Chua chon mon nao
                    AlertDialog.Builder alert = new AlertDialog.Builder(getContext());
                    alert.setTitle("Thông báo");
                    alert.setMessage("Hiện tại chưa có món ăn nào trong giỏ hàng, không thể gọi món!");
                    alert.setNegativeButton("OK",null);
                    alert.show();
                    return;
                }
                Log.d("KRT","getPreferencesMaBan: " + getPreferences("maBan"));
                if(getPreferences("maBan") == 0){
                    Intent intent = new Intent(getContext(),DSBanActivity.class);
                    startActivity(intent);
                }
                else {
                    int maBan = getPreferences("maBan");
                    int ttBan = getTTBanByMaBan(maBan);
                    if(ttBan == -1){
                        return;
                    }
                    if(ttBan == 0){
                        //Show thong bao
                        AlertDialog.Builder alert = new AlertDialog.Builder(getContext());
                        alert.setTitle("Thông báo");
                        alert.setMessage("Bàn chưa mở, không thể gọi món!");
                        alert.setNegativeButton("OK",null);
                        alert.show();
                        Log.d("KRT","Ban chua mo");
                        return;
                    }
                    int maHD = getMaHDByMB(maBan);
                    if(maHD == -1){
                        AlertDialog.Builder alert = new AlertDialog.Builder(getContext());
                        alert.setTitle("Thông báo");
                        alert.setMessage("Không tìm thấy hóa đơn ở bàn này, không thể gọi món!");
                        alert.setNegativeButton("OK",null);
                        alert.show();
                        Log.d("KRT","K tim thay hoa don");
                        return;
                    }
                    //Insert bang goi mon tai ban viet tiep di
                    for(CartItem cartItem: cart.getInstanceCart()){
                        insert(new GoiMonTaiBan(maHD,cartItem.getMaMon(),cartItem.getSoLuong(),0));
                    }
                    Log.d("KRT","Goi mon thanh cong");
                    cart.getInstanceCart().clear();
                    cartMonAdapter.notifyDataSetChanged();
                    Main2Activity.hideNumberCart();
                }
            }
        });
    }
    public void tinhThanhTien(){
        double tongTien =0;
        for(CartItem cart : cart.getInstanceCart()){
            tongTien += cart.getThanhTien();
        }
        Locale locale = new Locale("vi", "VN");
        NumberFormat numberFormat = NumberFormat.getCurrencyInstance(locale);
        txtThanhTienCart.setText(numberFormat.format(tongTien));
    }
    public int getPreferences(String key){
        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("caches",Context.MODE_PRIVATE);
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
    public int getTTBanByMaBan(int maBan){
        connection = connectionHelper.getConnection();
        if(connection != null){
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("Select TrangThai from Ban where MaBan='"+maBan+"'");
                if(resultSet != null){
                    if(!resultSet.next()){
                        Log.d("KRT","getTTBanByMaBan: MaBan: " + maBan + " ,Khong tim thay TT Ban");
                        return -1;
                    }
                    else{
                        Log.d("KRT","getTTBanByMaBan: MaBan: " + maBan + " ,TT Ban: " + resultSet.getInt(1));
                        return resultSet.getInt(1);
                    }
                }
            } catch (SQLException throwables) {
                throwables.printStackTrace();
            }

        }
        return -1;
    }
    private void insert(GoiMonTaiBan goiMonTaiBan){
        connection = connectionHelper.getConnection();
        if(connection != null){
            Statement statement;
            try {
                statement = connection.createStatement();
                statement.execute("Insert into GoiMonTaiBan Values('"+goiMonTaiBan.getMaHD()+"','"+goiMonTaiBan.getMaMon()+"','"+goiMonTaiBan.getSoLuong()+"','"+goiMonTaiBan.getTinhTrang()+"')");
            } catch (SQLException throwables) {
                throwables.printStackTrace();
            }
        }
        try {
            connection.close();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
    }
}
