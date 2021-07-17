package Adapter;

import android.content.Context;
import android.graphics.Paint;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.goimon.R;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Locale;

import Model.CartItem;
import Model.GoiMonTaiBan;
import Model.Mon;

public class NotificationAdapter extends RecyclerView.Adapter<NotificationAdapter.NotificationVH> {
    Context context;
    ArrayList<CartItem> monArrayList;

    public NotificationAdapter(Context context, ArrayList<CartItem> monArrayList) {
        this.context = context;
        this.monArrayList = monArrayList;
    }

    @NonNull
    @Override
    public NotificationVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View convertView = LayoutInflater.from(context).inflate(R.layout.item_notification,parent,false);
        return new NotificationVH(convertView);
    }

    @Override
    public void onBindViewHolder(@NonNull NotificationVH holder, int position) {
        CartItem mon = monArrayList.get(position);
        holder.imgAnhMonNotify.setImageBitmap(Mon.convertStringToBitmapFromAccess(context,mon.getAnh()));
        holder.txtTenMonNotify.setText(mon.getTenMon());
        Locale locale = new Locale("vi","VN");
        NumberFormat numberFormat = NumberFormat.getCurrencyInstance(locale);
        //
        if(mon.getGiaGoc() == mon.getGiaKM()){
            holder.txtGiaGocNotify.setText("");
            holder.txtGiaKMNotify.setText(numberFormat.format(mon.getGiaKM()));
        }
        else {
            holder.txtGiaGocNotify.setText(numberFormat.format(mon.getGiaGoc()));
            holder.txtGiaKMNotify.setText(numberFormat.format(mon.getGiaKM()));
        }
        holder.txtSoLuongNotify.setText("x"+ mon.getSoLuong());
    }

    @Override
    public int getItemCount() {
        return monArrayList.size();
    }

    class NotificationVH extends RecyclerView.ViewHolder {
        ImageView imgAnhMonNotify;
        TextView txtTenMonNotify, txtGiaKMNotify, txtGiaGocNotify,txtSoLuongNotify;
        public NotificationVH(@NonNull View itemView) {
            super(itemView);
            imgAnhMonNotify = itemView.findViewById(R.id.imgAnhmonNotify);
            txtTenMonNotify = itemView.findViewById(R.id.txtTenMonNotify);
            txtGiaKMNotify = itemView.findViewById(R.id.txtGiaKMNotify);
            txtGiaGocNotify = itemView.findViewById(R.id.txtGiaGocNotify);
            txtSoLuongNotify = itemView.findViewById(R.id.txtSoLuongNotify);
            txtGiaGocNotify.setPaintFlags(txtGiaGocNotify.getPaintFlags()| Paint.STRIKE_THRU_TEXT_FLAG);
        }
    }
}
