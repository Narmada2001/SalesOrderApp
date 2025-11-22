import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchClients } from "../redux/clientsSlice";
import { fetchItems } from "../redux/itemsSlice";
import { createOrder } from "../redux/ordersSlice";
import OrderLineRow from "./OrderLineRow";

export default function OrderForm() {
  const dispatch = useDispatch();

  const clients = useSelector((state) => state.clients.list);
  const items = useSelector((state) => state.items.list);

  const [selectedClient, setSelectedClient] = useState(null);
  const [lines, setLines] = useState([]);

  useEffect(() => {
    dispatch(fetchClients());
    dispatch(fetchItems());
  }, []);

  const addLine = () => {
    setLines([
      ...lines,
      {
        id: Date.now(),
        itemId: "",
        quantity: 1,
        taxRate: 0,
        price: 0,
        note: "",
      },
    ]);
  };

  const updateLine = (id, updated) => {
    setLines(lines.map((l) => (l.id === id ? updated : l)));
  };

  const removeLine = (id) => {
    setLines(lines.filter((l) => l.id !== id));
  };

  const calculateTotals = () => {
    let excl = 0,
      tax = 0,
      incl = 0;
    lines.forEach((l) => {
      const e = l.quantity * l.price;
      const t = (e * l.taxRate) / 100;
      excl += e;
      tax += t;
      incl += e + t;
    });
    return { excl, tax, incl };
  };

  const totals = calculateTotals();

  const saveOrder = () => {
    const payload = {
      clientId: selectedClient.id,
      orderDate: new Date().toISOString(),
      lines: lines.map((l) => ({
        itemId: l.itemId,
        quantity: l.quantity,
        taxRate: l.taxRate,
        price: l.price,
        note: l.note,
      })),
    };

    dispatch(createOrder(payload));
    alert("Order saved!");
  };

  return (
    <div className="p-6 bg-white shadow-md rounded">
      <h1 className="text-2xl font-bold mb-4">Create Sales Order</h1>

      {/* Customer */}
      <label className="font-semibold">Customer</label>
      <select
        className="border p-2 w-full mb-4"
        onChange={(e) =>
          setSelectedClient(clients.find((c) => c.id === Number(e.target.value)))
        }
      >
        <option value="">Select Customer</option>
        {clients.map((c) => (
          <option key={c.id} value={c.id}>
            {c.name}
          </option>
        ))}
      </select>

      {/* Address auto-fill */}
      {selectedClient && (
        <div className="mb-4">
          <p>Address 1: {selectedClient.address1}</p>
          <p>Address 2: {selectedClient.address2}</p>
          <p>City: {selectedClient.city}</p>
        </div>
      )}

      {/* Line Items */}
      <table className="w-full border mb-4">
        <thead>
          <tr className="bg-gray-200">
            <th className="p-2 border">Item</th>
            <th className="p-2 border">Price</th>
            <th className="p-2 border">Qty</th>
            <th className="p-2 border">Tax %</th>
            <th className="p-2 border">Excl</th>
            <th className="p-2 border">Tax</th>
            <th className="p-2 border">Incl</th>
            <th className="p-2 border"></th>
          </tr>
        </thead>
        <tbody>
          {lines.map((line) => (
            <OrderLineRow
              key={line.id}
              line={line}
              items={items}
              onChange={(updated) => updateLine(line.id, updated)}
              onRemove={() => removeLine(line.id)}
            />
          ))}
        </tbody>
      </table>

      <button
        className="bg-green-600 text-white px-4 py-2 rounded mb-4"
        onClick={addLine}
      >
        + Add Item
      </button>

      {/* Totals */}
      <div className="mt-4 p-4 border rounded bg-gray-50">
        <p>Total Excl: <strong>{totals.excl.toFixed(2)}</strong></p>
        <p>Total Tax: <strong>{totals.tax.toFixed(2)}</strong></p>
        <p>Total Incl: <strong>{totals.incl.toFixed(2)}</strong></p>
      </div>

      {/* Save */}
      <button
        className="bg-blue-600 text-white px-6 py-3 rounded mt-4"
        onClick={saveOrder}
      >
        Save Order
      </button>
    </div>
  );
}
