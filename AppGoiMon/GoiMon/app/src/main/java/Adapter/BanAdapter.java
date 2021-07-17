package Adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.example.goimon.R;

import java.util.List;

import Model.Ban;

public class BanAdapter extends ArrayAdapter<Ban> {
    public BanAdapter(@NonNull Context context, @NonNull List<Ban> objects) {
        super(context, 0, objects);
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        Ban ban = getItem(position);
        LayoutInflater layoutInflater = LayoutInflater.from(getContext());
        convertView = layoutInflater.inflate(R.layout.item_ban,parent,false);
        //ánh xạ
        //ImageView imgBan = convertView.findViewById(R.id.imgBan);
        TextView txtTenBan = convertView.findViewById(R.id.txtTenBan);
        // gán dữ liệu
//        if(ban.getTrangThai() == 1){
//            imgBan.setImageResource(R.drawable.bancokhach);
//        }
//        else{
//            imgBan.setImageResource(R.drawable.bantrong);
//        }
        txtTenBan.setText(ban.getTenBan());
        return convertView;
    }
}
