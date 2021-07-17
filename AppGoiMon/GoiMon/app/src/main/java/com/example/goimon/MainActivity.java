package com.example.goimon;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class MainActivity extends AppCompatActivity {
    Button btnClick;
    TextView txtShow;
    ConnectionHelper connectionHelper = new ConnectionHelper();
    Connection connection = null;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        //
        linkControl();
        //
        connection = connectionHelper.getConnection();
        //
        btnClick.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (connection!=null){
                    Statement statement = null;
                    try {
                        statement = connection.createStatement();
                        ResultSet resultSet = statement.executeQuery("Select * from Mon;");
                        while (resultSet.next()){
                            Log.d("KRT",resultSet.getString(4));
                        }
                    } catch (SQLException e) {
                        e.printStackTrace();
                    }
                }
                else {
                    txtShow.setText("Connection is null");
                }
            }
        });
    }
    private void linkControl(){
        btnClick = findViewById(R.id.btnClick);
        txtShow = findViewById(R.id.txtShow);
    }
//        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
//        StrictMode.setThreadPolicy(policy);
//        try {
//            Class.forName(Classes);
//            connection = DriverManager.getConnection(url,username,password);
//            txtShow.setText("SUCCESS");
//        } catch (ClassNotFoundException e) {
//            e.printStackTrace();
//            txtShow.setText("ERROR");
//        } catch (SQLException e) {
//            e.printStackTrace();
//            txtShow.setText("FAILURE");
//        }
//        btnClick.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View v) {
//                if (connection!=null){
//                    Statement statement = null;
//                    try {
//                        statement = connection.createStatement();
//                        ResultSet resultSet = statement.executeQuery("Select * from sach;");
//                        while (resultSet.next()){
//                            txtShow.setText(resultSet.getString(1));
//                        }
//                    } catch (SQLException e) {
//                        e.printStackTrace();
//                    }
//                }
//                else {
//                    txtShow.setText("Connection is null");
//                }
//            }
//
//        });
//    }


//        btnClick.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View v) {
//                ConnectionHelper con = new ConnectionHelper();
//                Connection conn = con.connect();
//                if(conn != null){
//                    Statement statement = null;
//                    try {
//                        ResultSet resultSet = statement.executeQuery("SELECT * FROM SACH");
//                        while (resultSet.next()){
//                            i[0]++;
//                        }
//                    } catch (SQLException throwables) {
//                        throwables.printStackTrace();
//                    }
//                    txtShow.setText(i[0] +"");
//                }
//            }
//        });
}