import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import api from "../services/api";

export const fetchItems = createAsyncThunk("items/fetch", async () => {
  const res = await api.get("/items");
  return res.data;
});

const itemsSlice = createSlice({
  name: "items",
  initialState: {
    list: [],
    loading: false,
  },
  extraReducers: (builder) => {
    builder.addCase(fetchItems.pending, (state) => {
      state.loading = true;
    });
    builder.addCase(fetchItems.fulfilled, (state, action) => {
      state.loading = false;
      state.list = action.payload;
    });
  },
});

export default itemsSlice.reducer;

