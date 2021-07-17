package com.example.goimon;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import android.graphics.Color;
import android.os.Bundle;
import android.view.MenuItem;
import android.widget.FrameLayout;

import com.google.android.material.badge.BadgeDrawable;
import com.google.android.material.bottomnavigation.BottomNavigationView;

import java.io.IOException;
import java.io.InputStream;

import Model.Cart;

public class Main2Activity extends AppCompatActivity {
    FrameLayout frameLayoutMain;
    public  static BottomNavigationView bottomNav;
    Cart cart;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);
        //ánh xạ
        linkWidget();
        //
        bottomNav.setOnNavigationItemSelectedListener(navigationItemSelectedListener);
        bottomNav.setSelectedItemId(R.id.mnNavMon);
        //showNumberCart(0);
        loadNumberCart();
    }
    private void linkWidget(){
        frameLayoutMain = findViewById(R.id.frameLayoutMain);
        bottomNav = findViewById(R.id.bottomNav);
    }
    private BottomNavigationView.OnNavigationItemSelectedListener navigationItemSelectedListener = new BottomNavigationView.OnNavigationItemSelectedListener() {
        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            Fragment fragment = null;
            switch (item.getItemId()){
                case R.id.mnNavMon:{
                    if(getSupportActionBar() != null){
                        getSupportActionBar().hide();
                    }
                    fragment = new FoodsFragment();
                    displayFragment(fragment);
                    return true;
                }
                case R.id.mnNavNotification:{
                    if(getSupportActionBar() != null){
                        getSupportActionBar().setTitle("Lịch sử gọi món");
                        getSupportActionBar().show();
                    }
                    fragment = new NotificationFragment();
                    displayFragment(fragment);
                    return true;
                }
                case R.id.mnNavCart: {
                    if (getSupportActionBar() != null) {
                        getSupportActionBar().setTitle("Gọi món");
                        getSupportActionBar().show();
                    }
                    fragment = new CartFragment();
                    displayFragment(fragment);
                    return true;
                }
            }
            return false;
        }
    };

   public void loadNumberCart(){
       int number = Cart.getInstanceCart().size();
       if(number > 0){
           showNumberCart(number);
       }
       else {
           hideNumberCart();
       }

   }

    private void displayFragment(Fragment fragment){
        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        //custom animation
        fragmentTransaction.replace(R.id.frameLayoutMain,fragment);
        //fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }
    public static void showNumberCart(int number){
        BadgeDrawable badgeDrawable = bottomNav.getOrCreateBadge(R.id.mnNavCart);
        badgeDrawable.setBackgroundColor(Color.RED);
        badgeDrawable.setBadgeTextColor(Color.WHITE);
        if(badgeDrawable.isVisible() == false){
            badgeDrawable.setVisible(true);
        }
        badgeDrawable.setNumber(number);
    }
    public static void hideNumberCart(){
        BadgeDrawable badgeDrawable = bottomNav.getOrCreateBadge(R.id.mnNavCart);
        if(badgeDrawable != null){
            badgeDrawable.setVisible(false);
            badgeDrawable.clearNumber();
        }
    }

    @Override
    protected void onRestart() {
        super.onRestart();
        loadNumberCart();
    }
}