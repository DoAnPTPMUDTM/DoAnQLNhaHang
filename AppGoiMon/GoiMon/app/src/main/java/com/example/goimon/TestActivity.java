package com.example.goimon;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Build;
import android.os.Bundle;
import android.util.Log;

import java.sql.Connection;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import Model.APIService;
import Model.DataService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TestActivity extends AppCompatActivity {
    ConnectionHelper connectionHelper = new ConnectionHelper();
    Connection connection;

    @RequiresApi(api = Build.VERSION_CODES.R)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test);
        //
        ArrayList<Integer> a = new ArrayList<>();
        a.add(1);
        a.add(2);
        a.add(3);
        DataService dataService = APIService.getService();
        Call<Void> callBack = dataService.test(a);
        callBack.enqueue(new Callback() {
            @Override
            public void onResponse(Call call, Response response) {
                Log.d("KRT",response.code() + "");
            }

            @Override
            public void onFailure(Call call, Throwable t) {
                Log.d("KRT",t.getMessage());
            }
        });



    }


}