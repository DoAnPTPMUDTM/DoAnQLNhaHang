package com.example.goimon;

import android.os.StrictMode;
import android.util.Log;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class ConnectionHelper {
    private static String ip = "192.168.1.8";//"192.168.2.9";//Thay đổi ipV4 của máy
    private static String port = "1433";//Thay đổi port
    private static String Classes = "net.sourceforge.jtds.jdbc.Driver";
    private static String database = "QuanLyNhaHang";//Tên database
    private static String username = "sa";//username
    private static String password = "0905213883";//password
    private static String url = "jdbc:jtds:sqlserver://"+ip+":"+port+"/"+database;
    private Connection connection = null;

    public Connection getConnection() {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);
        try {
            Class.forName(Classes);
            connection = DriverManager.getConnection(url,username,password);
            return connection;
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
            return null;
        } catch (SQLException e) {
            e.printStackTrace();
            return null;
        }
    }
}
