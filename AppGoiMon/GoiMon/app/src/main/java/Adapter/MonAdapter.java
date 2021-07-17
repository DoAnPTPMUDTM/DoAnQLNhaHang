package Adapter;

import android.content.Context;
import android.graphics.Paint;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.goimon.R;

import java.io.Serializable;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Locale;

import Model.Mon;
import Model.OnCallBack;

public class MonAdapter extends RecyclerView.Adapter<MonAdapter.MonViewHolder> implements Serializable {
    Context context;
    ArrayList<Mon> monArrayList;
    OnCallBack itemRecyclerViewListener;
    Mon convertAnh = new Mon();

    public MonAdapter(Context context, ArrayList<Mon> monArrayList, OnCallBack itemRecyclerViewListener) {
        this.context = context;
        this.monArrayList = monArrayList;
        this.itemRecyclerViewListener = itemRecyclerViewListener;
    }

    @NonNull
    @Override
    public MonViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View convertView = LayoutInflater.from(context).inflate(R.layout.item_mon,parent,false);
        return new MonViewHolder(convertView);
    }

    @Override
    public void onBindViewHolder(@NonNull MonViewHolder holder, int position) {
        Mon mon = monArrayList.get(position);
        holder.imgAnhMon.setImageBitmap(convertAnh.convertStringToBitmapFromAccess(context,mon.getAnhMon()));
        holder.txtTenMonAn.setText(mon.getTenMon());
        //holder.txtPTGiamGia.setText(mon.getMaKM());
        //
        Locale locale = new Locale("vi","VN");
        NumberFormat numberFormat = NumberFormat.getCurrencyInstance(locale);
        //
        holder.txtGiaGoc.setText(numberFormat.format(mon.getGiaGoc()));
        holder.txtGiaKM.setText(numberFormat.format(mon.getGiaKM()));
        if(mon.getGiaGoc() == mon.getGiaKM()){
            holder.imgPTGiamGia.setVisibility(View.GONE);
            holder.txtPTGiamGia.setVisibility(View.GONE);
            //test custom lại
            holder.txtTieuDeGiaKM.setText("Giá:");
            holder.txtTieuDeGia.setVisibility(View.GONE);
            holder.txtGiaGoc.setVisibility(View.GONE);
        }
        else {
            //holder.imgPTGiamGia.setVisibility(View.INVISIBLE);
            double giaGiam = mon.getGiaGoc() - mon.getGiaKM();
            double giamGia = giaGiam / mon.getGiaGoc();
            int castInt = (int)(giamGia * 100);
            String ptGiamGia = "-" + castInt +"%";
            holder.txtPTGiamGia.setText(ptGiamGia);
        }
    }

    @Override
    public int getItemCount() {
        return monArrayList.size();
    }

    class MonViewHolder extends RecyclerView.ViewHolder{
        ImageView imgAnhMon,imgPTGiamGia;
        TextView txtPTGiamGia, txtTenMonAn, txtGiaGoc, txtGiaKM,txtTieuDeGia,txtTieuDeGiaKM;

        public MonViewHolder(@NonNull View itemView) {
            super(itemView);
            imgAnhMon = itemView.findViewById(R.id.imgAnhMon);
            imgPTGiamGia = itemView.findViewById(R.id.imgPTGiamGia);
            txtPTGiamGia = itemView.findViewById(R.id.txtPTGiamGia);
            txtTenMonAn = itemView.findViewById(R.id.txtTenMonAn);
            txtGiaGoc = itemView.findViewById(R.id.txtGiaGoc);
            txtGiaKM = itemView.findViewById(R.id.txtGiaKM);
            //
            txtTieuDeGia = itemView.findViewById(R.id.txtTieuDeGia);
            txtTieuDeGiaKM = itemView.findViewById(R.id.txtTieuDeGiaKM);
            txtGiaGoc.setPaintFlags(txtGiaGoc.getPaintFlags()| Paint.STRIKE_THRU_TEXT_FLAG);
            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    itemRecyclerViewListener.onItemRecyclerViewClick(getPosition());
                }
            });
        }
    }
}
