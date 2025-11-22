import React from "react";

export default function OrderLineRow({ line, items, onChange, onRemove }) {
  const selectedItem = items.find((i) => i.id === Number(line.itemId));

  const excl = line.quantity * line.price;
  const tax = (excl * line.taxRate) / 100;
  const incl = excl + tax;

  return (
    <tr>
      <td className="border p-2">
        <select
          className="border p-1"
          value={line.itemId}
          onChange={(e) => {
            const item = items.find((i) => i.id === Number(e.target.value));
            onChange({
              ...line,
              itemId: item.id,
              price: item.price,
            });
          }}
        >
          <option value="">Select</option>
          {items.map((it) => (
            <option key={it.id} value={it.id}>
              {it.code} - {it.description}
            </option>
          ))}
        </select>
      </td>

      <td className="border p-2">{line.price}</td>

      <td className="border p-2">
        <input
          type="number"
          className="border p-1 w-20"
          value={line.quantity}
          onChange={(e) =>
            onChange({ ...line, quantity: Number(e.target.value) })
          }
        />
      </td>

      <td className="border p-2">
        <input
          type="number"
          className="border p-1 w-20"
          value={line.taxRate}
          onChange={(e) =>
            onChange({ ...line, taxRate: Number(e.target.value) })
          }
        />
      </td>

      <td className="border p-2">{excl.toFixed(2)}</td>
      <td className="border p-2">{tax.toFixed(2)}</td>
      <td className="border p-2">{incl.toFixed(2)}</td>

      <td className="border p-2">
        <button
          onClick={onRemove}
          className="bg-red-500 text-white px-2 py-1 rounded"
        >
          X
        </button>
      </td>
    </tr>
  );
}
