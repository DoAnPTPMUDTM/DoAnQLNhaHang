package com.example.goimon;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Dialog;
import android.app.ListActivity;
import android.app.SearchManager;
import android.content.Intent;
import android.graphics.Paint;
import android.os.Bundle;
import android.provider.SearchRecentSuggestions;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.SearchView;
import android.widget.TextView;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Locale;

import Adapter.SearchMonAdapter;
import Model.Cart;
import Model.CartItem;
import Model.Mon;

public class SearchActivity extends AppCompatActivity {
    SearchView searchView;
    TextView txtKetQuaSearch,txtTenMonDialog,txtSoLuongMon,txtGiaGocDialog,txtGiaKMDialog;
    ListView lstSearchDSMon;
    SearchMonAdapter searchMonAdapter;
    ArrayList<Mon> monArrayList;
    Connection connection =null;
    ConnectionHelper connectionHelper = new ConnectionHelper();
    Cart cart;
    CartItem cartItem;
    ImageView imgAdd, imgSub,imgAnhMonDialog;
    Button btnOK, btnHuy;
    Mon mon;
    int number = 1;
    RelativeLayout relativeSearch;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);
        linkControl();
        relativeSearch = findViewById(R.id.relativeSearch);
        //
//        Intent intent = getIntent();
//        if(Intent.ACTION_SEARCH.equals(intent.getAction())){
//            String query = intent.getStringExtra(SearchManager.QUERY);
//            SearchRecentSuggestions suggestions = new SearchRecentSuggestions(this, MySuggestionProvider.AUTHORITY, MySuggestionProvider.MODE);
//            suggestions.saveRecentQuery(query,null);
//            Log.d("KRT",""+SearchManager.QUERY);
//        }
        connection = connectionHelper.getConnection();
        //ListView
        monArrayList = new ArrayList<>();
        //Coi lai
        searchMonAdapter = new SearchMonAdapter(SearchActivity.this, monArrayList);
        lstSearchDSMon.setAdapter(searchMonAdapter);
        searchMonAdapter.notifyDataSetChanged();
        Log.d("Vy",""+ monArrayList.size());
        //Search
        searchView.setFocusable(true);
        searchView.setIconified(false);
        searchView.requestFocusFromTouch();
        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                return false;
            }

            @Override
            public boolean onQueryTextChange(String newText) {
                searchMonAn(newText);
                return false;
            }
        });
        //lsstView click.
        lstSearchDSMon.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                //
                Toast.makeText(SearchActivity.this, ""+monArrayList.get(position).getMaMon(), Toast.LENGTH_SHORT).show();
                Locale locale = new Locale("vi","VN");
                NumberFormat numberFormat = NumberFormat.getCurrencyInstance(locale);
                //
                Dialog dialog = new Dialog(SearchActivity.this);
                dialog.setContentView(R.layout.custom_dialog_mon_selected);
                number = 1;
                //anh xa
                txtTenMonDialog = dialog.findViewById(R.id.txtTenMonDialog);
                txtSoLuongMon = dialog.findViewById(R.id.txtSoLuongMon);
                txtGiaGocDialog = dialog.findViewById(R.id.txtGiaGocDialog);
                txtGiaKMDialog = dialog.findViewById(R.id.txtGiaKMDialog);
                imgAdd = dialog.findViewById(R.id.imgAdd);
                imgSub = dialog.findViewById(R.id.imgSub);
                imgAnhMonDialog = dialog.findViewById(R.id.imgAnhMonDialog);
                btnOK = dialog.findViewById(R.id.btnOK);
                btnHuy = dialog.findViewById(R.id.btnHuy);
                //gán dữ liệu
                txtTenMonDialog.setText(monArrayList.get(position).getTenMon());
                imgAnhMonDialog.setImageBitmap(mon.convertStringToBitmapFromAccess(SearchActivity.this,monArrayList.get(position).getAnhMon()));
                if(monArrayList.get(position).getGiaGoc() == monArrayList.get(position).getGiaKM()){
                    txtGiaGocDialog.setText("");
                    txtGiaKMDialog.setText(numberFormat.format(monArrayList.get(position).getGiaKM()));
                }
                else{
                    txtGiaGocDialog.setText(numberFormat.format(monArrayList.get(position).getGiaGoc()));
                    txtGiaKMDialog.setText(numberFormat.format(monArrayList.get(position).getGiaKM()));
                }
                txtGiaGocDialog.setPaintFlags(txtGiaGocDialog.getPaintFlags()| Paint.STRIKE_THRU_TEXT_FLAG);
                dialog.getWindow().setLayout(WindowManager.LayoutParams.MATCH_PARENT, WindowManager.LayoutParams.WRAP_CONTENT);
                dialog.show();
                //button Hủy setOnClick
                btnHuy.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        dialog.cancel();
                    }
                });
                //button Ok set onclick
                btnOK.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        cartItem = new CartItem(monArrayList.get(position).getMaMon(),monArrayList.get(position).getTenMon(),monArrayList.get(position).getAnhMon(),monArrayList.get(position).getGiaGoc(),monArrayList.get(position).getGiaKM());
                        int soLuong = Integer.parseInt(txtSoLuongMon.getText().toString());
                        if(cart.getInstanceCart().size() > 0){
                            boolean check = true;
                            for (int i =0; i < cart.getInstanceCart().size();i++){
                                if(monArrayList.get(position).getMaMon() == cart.getInstanceCart().get(i).getMaMon()){
                                    int soLuongHT = cart.getInstanceCart().get(i).getSoLuong();
                                    cart.getInstanceCart().get(i).setSoLuong(soLuong + soLuongHT);
                                    check = false;
                                    break;
                                }
                            }
                            if(check){
                                cartItem.setSoLuong(soLuong);
                                cart.getInstanceCart().add(cartItem);
                                Main2Activity.showNumberCart(soLuong);
                            }
                        }
                        else {
                            cartItem.setSoLuong(soLuong);
                            cart.getInstanceCart().add(cartItem);
                            Main2Activity.showNumberCart(soLuong);
                        }
                        Toast.makeText(SearchActivity.this, "Thêm món ăn "+ monArrayList.get(position).getTenMon() + " thành công!", Toast.LENGTH_LONG).show();
                        dialog.cancel();
                    }
                });
                //imgSub giảm sl món
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
                //imgAdd thêm sl món
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

    }
    private void searchMonAn(String keySearch){
        connection = connectionHelper.getConnection();
        ArrayList<Mon> arrayList = new ArrayList<>();
        if(connection!= null){
            Statement statement = null;
            try {
                statement = connection.createStatement();
                ResultSet resultSet = statement.executeQuery("Select * from Mon Where TenMon LIKE '%"+keySearch+"%'");
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
                        //
                    }
                    //
                    if(arrayList.size() > 0){
                        monArrayList.clear();
                        monArrayList.addAll(arrayList);
                        searchMonAdapter.notifyDataSetChanged();
                        lstSearchDSMon.setVisibility(View.VISIBLE);
                        txtKetQuaSearch.setVisibility(View.GONE);
                    }
                    else{
                        lstSearchDSMon.setVisibility(View.GONE);
                        txtKetQuaSearch.setVisibility(View.VISIBLE);
                    }
                    if(keySearch.isEmpty()){
                        //
                        lstSearchDSMon.setVisibility(View.GONE);
                    }
                }

            } catch (SQLException throwables) {
                throwables.printStackTrace();
            }

        }
        else{
            Toast.makeText(this, "Connection is null", Toast.LENGTH_SHORT).show();
        }
        try {
            connection.close();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
    }
    private void linkControl(){
        searchView = findViewById(R.id.searchView);
        txtKetQuaSearch = findViewById(R.id.txtKetQuaSearch);
        lstSearchDSMon = findViewById(R.id.lstSearchDSMon);
    }
    //back mnuDSMon.


    @Override
    public void onBackPressed() {
        super.onBackPressed();
        this.finish();
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()){
            case R.id.mnNavMon:
                this.finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }

    }
}