package Model;

import java.util.ArrayList;

public class History {
    private static ArrayList<CartItem> cart = null;

    public History() {
    }
    public static synchronized ArrayList<CartItem> getInstanceHistory(){
        if(cart == null){
            cart = new ArrayList<>();
        }
        return (cart);
    }
}
