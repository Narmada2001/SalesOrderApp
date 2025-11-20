import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import api from "../services/api";

export const fetchOrders = createAsyncThunk("orders/fetch", async () => {
  const res = await api.get("/salesorders");
  return res.data;
});

export const createOrder = createAsyncThunk(
  "orders/create",
  async (payload) => {
    const res = await api.post("/salesorders", payload);
    return res.data;
  }
);

const ordersSlice = createSlice({
  name: "orders",
  initialState: {
    list: [],
    loading: false,
  },
  extraReducers: (builder) => {
    builder.addCase(fetchOrders.fulfilled, (state, action) => {
      state.list = action.payload;
    });
  },
});

export default ordersSlice.reducer;

