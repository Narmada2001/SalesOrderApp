import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import api from "../services/api";

export const fetchClients = createAsyncThunk("clients/fetch", async () => {
  const res = await api.get("/clients");
  return res.data;
});

const clientsSlice = createSlice({
  name: "clients",
  initialState: {
    list: [],
    loading: false,
  },
  extraReducers: (builder) => {
    builder.addCase(fetchClients.pending, (state) => {
      state.loading = true;
    });
    builder.addCase(fetchClients.fulfilled, (state, action) => {
      state.loading = false;
      state.list = action.payload;
    });
  },
});

export default clientsSlice.reducer;

