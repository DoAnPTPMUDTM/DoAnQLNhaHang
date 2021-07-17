package Adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.example.goimon.R;

import java.util.List;

import Model.NhomMon;

public class NhomMonAdapter extends ArrayAdapter<NhomMon> {
    public NhomMonAdapter(@NonNull Context context, @NonNull List<NhomMon> objects) {
        super(context, 0, objects);
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        NhomMon nhomMon = getItem(position);
        LayoutInflater layoutInflater = LayoutInflater.from(getContext());
        convertView = layoutInflater.inflate(R.layout.item_selected,parent,false);
        //
        //if(nhomMon != null){
            TextView txtTenNhomMonSpinner_Selected = convertView.findViewById(R.id.txtTenNhomMonSpinner_Selected);
            txtTenNhomMonSpinner_Selected.setText(nhomMon.getTenNhom());
        //}
        return convertView;
    }

    @Override
    public View getDropDownView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        NhomMon nhomMon = getItem(position);
        LayoutInflater layoutInflater = LayoutInflater.from(getContext());
        convertView = layoutInflater.inflate(R.layout.item_spinner,parent,false);
        //anh xa
        ///if(nhomMon != null){
            TextView txtTenNhomMonSpinner = convertView.findViewById(R.id.txtTenNhomMonSpinner);
            txtTenNhomMonSpinner.setText(nhomMon.getTenNhom());
        //}
        return convertView;

    }

}
