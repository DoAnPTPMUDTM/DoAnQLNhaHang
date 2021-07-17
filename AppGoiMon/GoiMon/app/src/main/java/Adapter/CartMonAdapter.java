package Adapter;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
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
import Model.Mon;
import Model.OnCallBack;
import Model.OnCartCallBack;

public class CartMonAdapter extends RecyclerView.Adapter<CartMonAdapter.MonViewHolder> {
    Context context;
    ArrayList<CartItem> cartItemArrayList;
    OnCartCallBack onCartCallBack;

    public CartMonAdapter(Context context, ArrayList<CartItem> cartItemArrayList, OnCartCallBack onCartCallBack) {
        this.context = context;
        this.cartItemArrayList = cartItemArrayList;
        this.onCartCallBack = onCartCallBack;
    }

    @NonNull
    @Override
    public MonViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View convertView = LayoutInflater.from(context).inflate(R.layout.item_cart,parent,false);
        return new MonViewHolder(convertView);
    }

    @Override
    public void onBindViewHolder(@NonNull MonViewHolder holder, int position) {
        CartItem cartItem = cartItemArrayList.get(position);
        Mon mon = new Mon();
        holder.imgAnhMonCart.setImageBitmap(mon.convertStringToBitmapFromAccess(context,cartItem.getAnh()));
        holder.txtTenMonCart.setText(cartItem.getTenMon());
        //
        Locale locale = new Locale("vi","VN");
        NumberFormat numberFormat = NumberFormat.getCurrencyInstance(locale);
        //
        if(cartItem.getGiaGoc() == cartItem.getGiaKM()){
            holder.txtGiaGocCart.setText("");
            holder.txtGiaKMCart.setText(numberFormat.format(cartItem.getGiaKM()));
        }
        else {
            holder.txtGiaGocCart.setText(numberFormat.format(cartItem.getGiaGoc()));
            holder.txtGiaKMCart.setText(numberFormat.format(cartItem.getGiaKM()));
        }
        holder.txtSoLuongCart.setText(String.valueOf(cartItem.getSoLuong()));
        //img Add, img sub
        holder.imgSubCart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onCartCallBack.OnButtonCartCallBack();
                int soLuong = cartItemArrayList.get(holder.getAdapterPosition()).getSoLuong();
                if(soLuong > 1){
                    soLuong--;
                    holder.txtSoLuongCart.setText(String.valueOf(soLuong));
                    if(soLuong==1){
                        holder.imgSubCart.setEnabled(false);
                    }
                    cartItemArrayList.get(holder.getAdapterPosition()).setSoLuong(soLuong);
                    onCartCallBack.OnButtonCartCallBack();
                }
            }
        });
        holder.imgAddCart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int soLuong = cartItemArrayList.get(holder.getAdapterPosition()).getSoLuong();
                soLuong++;
                if(soLuong > 1){
                    holder.imgSubCart.setEnabled(true);
                }
                holder.txtSoLuongCart.setText(String.valueOf(soLuong));
                cartItemArrayList.get(holder.getAdapterPosition()).setSoLuong(soLuong);
                onCartCallBack.OnButtonCartCallBack();
            }
        });
        holder.imgDeleteCart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                AlertDialog.Builder alert = new AlertDialog.Builder(context);
                alert.setTitle("Thông báo!");
                alert.setMessage("Bạn có muốn xóa "+cartItemArrayList.get(holder.getAdapterPosition()).getTenMon()+" ra khỏi giỏ hàng không?");
                alert.setPositiveButton("Đồng ý", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        cartItemArrayList.remove(holder.getAdapterPosition());
                        //nhớ notifyitemRemoved.
                        notifyItemRemoved(holder.getAdapterPosition());
                        onCartCallBack.OnButtonDeleteCartCallBack();
                    }
                });
                alert.setNegativeButton("Hủy", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.cancel();
                    }
                });
                AlertDialog dialog = alert.create();
                dialog.show();
            }
        });
    }

    @Override
    public int getItemCount() {
        if(cartItemArrayList != null){
            return cartItemArrayList.size();
        }
        return 0;
    }

    class MonViewHolder extends RecyclerView.ViewHolder {
        ImageView imgAnhMonCart,imgAddCart,imgSubCart,imgDeleteCart;
        TextView txtTenMonCart,txtGiaKMCart,txtGiaGocCart,txtSoLuongCart;
        public MonViewHolder(@NonNull View itemView) {
            super(itemView);
            imgAnhMonCart = itemView.findViewById(R.id.imgAnhMonCart);
            imgAddCart = itemView.findViewById(R.id.imgAddCart);
            imgSubCart = itemView.findViewById(R.id.imgSubCart);
            imgDeleteCart = itemView.findViewById(R.id.imgDeleteCart);
            txtSoLuongCart = itemView.findViewById(R.id.txtSoLuongCart);
            txtTenMonCart = itemView.findViewById(R.id.txtTenMonCart);
            txtGiaKMCart = itemView.findViewById(R.id.txtGiaKMCart);
            txtGiaGocCart = itemView.findViewById(R.id.txtGiaGocCart);
            txtGiaGocCart.setPaintFlags(txtGiaGocCart.getPaintFlags()| Paint.STRIKE_THRU_TEXT_FLAG);
        }
    }
}
