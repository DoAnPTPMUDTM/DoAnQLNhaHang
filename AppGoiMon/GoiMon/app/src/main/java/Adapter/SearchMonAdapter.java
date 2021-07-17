package Adapter;

import android.content.Context;
import android.graphics.Paint;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.example.goimon.R;

import org.w3c.dom.Text;

import java.text.NumberFormat;
import java.util.List;
import java.util.Locale;

import Model.Mon;

public class SearchMonAdapter extends ArrayAdapter<Mon> {

    public SearchMonAdapter(@NonNull Context context, @NonNull List<Mon> objects) {
        super(context, 0, objects);
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        Mon mon = getItem(position);

        LayoutInflater layoutInflater = LayoutInflater.from(getContext());
        convertView = layoutInflater.inflate(R.layout.item_search,parent,false);
        //anhs xa
        ImageView imgAnhMonSearch = convertView.findViewById(R.id.imgAnhMonSearch);
        TextView txtTenMonSearch = convertView.findViewById(R.id.txtTenMonSearch);
        TextView txtGiaKMSearch = convertView.findViewById(R.id.txtGiaKMSearch);
        TextView txtGiaGocSearch = convertView.findViewById(R.id.txtGiaGocSearch);
        txtGiaGocSearch.setPaintFlags(txtGiaGocSearch.getPaintFlags()| Paint.STRIKE_THRU_TEXT_FLAG);
        //gan du lieu
        imgAnhMonSearch.setImageBitmap(Mon.convertStringToBitmapFromAccess(getContext(),mon.getAnhMon()));
        txtTenMonSearch.setText(mon.getTenMon());
        //
        Locale locale = new Locale("vi","VN");
        NumberFormat numberFormat = NumberFormat.getCurrencyInstance(locale);
        //
        txtGiaKMSearch.setText(numberFormat.format(mon.getGiaKM()));
        txtGiaGocSearch.setText(numberFormat.format(mon.getGiaGoc()));
        if(mon.getGiaGoc() == mon.getGiaKM()){
            txtGiaGocSearch.setText("");
        }
        return convertView;
    }
}
