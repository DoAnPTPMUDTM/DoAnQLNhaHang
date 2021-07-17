package com.example.goimon;

import android.app.Dialog;
import android.content.Intent;
import android.graphics.Paint;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.GridView;
import android.widget.ImageView;
import android.widget.SearchView;
import android.widget.Spinner;
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
import Adapter.MonAdapter;
import Adapter.NhomMonAdapter;
import Model.Cart;
import Model.CartItem;
import Model.Mon;
import Model.NhomMon;
import Model.OnCallBack;

public class FoodsFragment extends Fragment {
    RecyclerView recyclerMonAn;
    MonAdapter monAdapter;
    ArrayList<Mon> arrayList;
    ArrayList<NhomMon> nhomMonArrayList;
    NhomMonAdapter nhomMonAdapter;
    EditText searchView;
    TextView txtTenMonDialog,txtSoLuongMon,txtGiaGocDialog,txtGiaKMDialog;
    Button btnOK, btnHuy;
    Spinner snNhomMon;
    ImageView imgSub, imgAdd,imgAnhMonDialog;
    int number = 1;
    Cart cart = new Cart();
    CartItem cartItem;
    ConnectionHelper connectionHelper = new ConnectionHelper();
    Connection connection;
    Mon mon;
    //int maMon;

    public FoodsFragment() {
        // Required empty public constructor
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //connection = connectionHelper.getConnection();
        }
    

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_foods, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        recyclerMonAn = view.findViewById(R.id.recyclerMonAn);
        searchView = view.findViewById(R.id.searchView);
        snNhomMon = view.findViewById(R.id.snNhomMon);
        searchView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getContext(), SearchActivity.class);
                startActivity(intent);
            }
        });
        //Spinner

        nhomMonArrayList = getAllNhomMon();
        nhomMonAdapter = new NhomMonAdapter(getContext(),nhomMonArrayList);
        snNhomMon.setAdapter(nhomMonAdapter);
        nhomMonAdapter.notifyDataSetChanged();
        snNhomMon.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                snNhomMon.setSelected(true);
                int maNhom = nhomMonArrayList.get(position).getMaNhom();
                Toast.makeText(getContext(), ""+maNhom, Toast.LENGTH_SHORT).show();
                if(maNhom == 0){
                    arrayList = getAllMon();
                }
                else {
                    arrayList = getMonAnByMaNhom(maNhom);
                }
                monAdapter = new MonAdapter(getContext(), arrayList, new OnCallBack() {
                    @Override
                    public void onItemRecyclerViewClick(int position) {
                        //opendialog khi chọn món ăn
                        //Toast.makeText(getContext(), ""+arrayList.get(position).getTenMon(), Toast.LENGTH_SHORT).show();
                        //
                        Locale locale = new Locale("vi","VN");
                        NumberFormat numberFormat = NumberFormat.getCurrencyInstance(locale);
                        Dialog dialog = new Dialog(getContext());
                        number = 1;
                        //dialog.setCanceledOnTouchOutside(false);
                        dialog.setContentView(R.layout.custom_dialog_mon_selected);
                        txtTenMonDialog = dialog.findViewById(R.id.txtTenMonDialog);
                        txtSoLuongMon = dialog.findViewById(R.id.txtSoLuongMon);
                        txtGiaGocDialog = dialog.findViewById(R.id.txtGiaGocDialog);
                        txtGiaKMDialog = dialog.findViewById(R.id.txtGiaKMDialog);
                        imgAdd = dialog.findViewById(R.id.imgAdd);
                        imgSub = dialog.findViewById(R.id.imgSub);
                        imgAnhMonDialog = dialog.findViewById(R.id.imgAnhMonDialog);
                        btnOK = dialog.findViewById(R.id.btnOK);
                        btnHuy = dialog.findViewById(R.id.btnHuy);
                        // gán dữ liệu
                        txtTenMonDialog.setText(arrayList.get(position).getTenMon());
                        imgAnhMonDialog.setImageBitmap(mon.convertStringToBitmapFromAccess(getContext(),arrayList.get(position).getAnhMon()));
                        if(arrayList.get(position).getGiaGoc() == arrayList.get(position).getGiaKM()){
                            txtGiaGocDialog.setText("");
                            txtGiaKMDialog.setText(numberFormat.format(arrayList.get(position).getGiaKM()));
                        }
                        else{
                            txtGiaGocDialog.setText(numberFormat.format(arrayList.get(position).getGiaGoc()));
                            txtGiaKMDialog.setText(numberFormat.format(arrayList.get(position).getGiaKM()));
                        }
                        txtGiaGocDialog.setPaintFlags(txtGiaGocDialog.getPaintFlags()| Paint.STRIKE_THRU_TEXT_FLAG);
                        dialog.getWindow().setLayout(WindowManager.LayoutParams.MATCH_PARENT, WindowManager.LayoutParams.WRAP_CONTENT);
                        dialog.show();
                        //event click add, sub;
                        //Toast.makeText(getContext(), ""+arrayList.get(position).getMaMon(), Toast.LENGTH_SHORT).show();

                        btnHuy.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                dialog.cancel();
                            }
                        });
                        //ok chọn món
                        btnOK.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                cartItem = new CartItem(arrayList.get(position).getMaMon(),arrayList.get(position).getTenMon(),arrayList.get(position).getAnhMon(),arrayList.get(position).getGiaGoc(),arrayList.get(position).getGiaKM());
                                int soLuong = Integer.parseInt(txtSoLuongMon.getText().toString());
                                if(cart.getInstanceCart().size() > 0){
                                    boolean check = true;
                                    for(int i =0; i < cart.getInstanceCart().size();i++){
                                        if(arrayList.get(position).getMaMon() == cart.getInstanceCart().get(i).getMaMon()){
                                            int soLuongHT = cart.getInstanceCart().get(i).getSoLuong();
                                            cart.getInstanceCart().get(i).setSoLuong(soLuongHT+soLuong);
                                            check = false;
                                            break;
                                        }
                                    }
                                    if(check){
                                        cartItem.setSoLuong(soLuong);
                                        cart.getInstanceCart().add(cartItem);
                                        Main2Activity.showNumberCart(cart.getInstanceCart().size());
                                    }
                                }
                                else {
                                    cartItem.setSoLuong(soLuong);
                                    cart.getInstanceCart().add(cartItem);
                                    Main2Activity.showNumberCart(cart.getInstanceCart().size());
                                }
                                Toast.makeText(getContext(), "Thêm món ăn "+ arrayList.get(position).getTenMon() + " thành công!", Toast.LENGTH_LONG).show();
                                dialog.cancel();
                                //Toast.makeText(getContext(), "ma mon"+arrayList.get(position).getMaMon()+"Ten mon"+arrayList.get(position).getTenMon()+"Anh mon"+arrayList.get(position).getAnhMon()+"Gia goc"+arrayList.get(position).getGiaGoc()+"KM"+arrayList.get(position).getGiaKM(), Toast.LENGTH_SHORT).show();
                                //Log.d("Vy","CartFragment - So luong mat hang trong gio hang sau khi them: " + cart.getInstanceCart().size() +"Mã món ăn:"+cart.getInstanceCart().get(position).getMaMon()+"Tên món ăn:"+cart.getInstanceCart().get(position).getTenMon());
                            }
                        });
                        //event sub
                        imgSub.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                if(number > 1){
                                    number--;
                                    txtSoLuongMon.setText(String.valueOf(number));
                                    if(number == 1){
                                        imgSub.setEnabled(false);
                                    }
                                }
                            }
                        });
                        //event click add
                        imgAdd.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                number++;
                                if(number > 1){
                                    imgSub.setEnabled(true);
                                }
                                txtSoLuongMon.setText(String.valueOf(number));
                            }
                        });
                    }
                });
                recyclerMonAn.setLayoutManager(new GridLayoutManager(getContext(),2));
                recyclerMonAn.setAdapter(monAdapter);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }
    private ArrayList<Mon> getAllMon(){
        connection = connectionHelper.getConnection();
        arrayList = new ArrayList<>();
        if(connection != null){
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("Select * from Mon");
                if(resultSet != null){
                    while (resultSet.next()){
                         int maMon = resultSet.getInt(1);
                         int maNhom=resultSet.getInt(2);
                         int maDVT=resultSet.getInt(3);
                         String tenMon = resultSet.getString(4);
                         String anhMon = resultSet.getString(5);
                         double giaGoc = resultSet.getDouble(6);
                         double giaKM = resultSet.getDouble(7);
                         int maKM = resultSet.getInt(8);
                         arrayList.add(new Mon(maMon,maNhom,maDVT,tenMon,anhMon,giaGoc,giaKM,maKM));
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
        return arrayList;
    }
    private ArrayList<NhomMon> getAllNhomMon(){
        connection = connectionHelper.getConnection();
        nhomMonArrayList = new ArrayList<>();
        nhomMonArrayList.add(0,new NhomMon(0,"Tất cả"));

        if(connection!= null){
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("Select * from NhomMon");
                if(resultSet != null){
                    while (resultSet.next()){
                        int maNhom = resultSet.getInt(1);
                        String tenNhom = resultSet.getString(2);
                        nhomMonArrayList.add(new NhomMon(maNhom,tenNhom));
                    }
                }
            else {
                    Toast.makeText(getContext(), "Connect is null", Toast.LENGTH_SHORT).show();
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
        return nhomMonArrayList;
    }
    //getMonAnByMaNhom
    private ArrayList<Mon> getMonAnByMaNhom(int maNhom){
        connection = connectionHelper.getConnection();
        arrayList = new ArrayList<>();
        if(connection!= null){
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("Select * from Mon Where MaNhom='"+maNhom+"'");
                if(resultSet != null){
                    while (resultSet.next()){
                        int maMon = resultSet.getInt(1);
                        int MaNhom=resultSet.getInt(2);
                        int maDVT=resultSet.getInt(3);
                        String tenMon = resultSet.getString(4);
                        String anhMon = resultSet.getString(5);
                        double giaGoc = resultSet.getDouble(6);
                        double giaKM = resultSet.getDouble(7);
                        int maKM = resultSet.getInt(8);
                        arrayList.add(new Mon(maMon,MaNhom,maDVT,tenMon,anhMon,giaGoc,giaKM,maKM));
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
        return arrayList;
    }
}