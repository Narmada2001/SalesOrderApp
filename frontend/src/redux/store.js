import { configureStore } from "@reduxjs/toolkit";
import clientsReducer from "./clientsSlice";
import itemsReducer from "./itemsSlice";
import ordersReducer from "./ordersSlice";

export default configureStore({
  reducer: { clients: clientsReducer, items: itemsReducer, orders: ordersReducer },
});
