import { useEffect, useState } from 'react';
import { deviceApi } from '../api/api';
import type { Device, CreateDevice } from '../api/api';

const categories = ['Laptop', 'Desktop', 'Monitor', 'Keyboard', 'Mouse', 'Headset', 'Phone', 'Tablet', 'Printer', 'Other'];
const statuses = ['Available', 'Assigned', 'Under Repair', 'Retired'];

const emptyForm: CreateDevice = {
  name: '', serialNumber: '', category: 0, status: 0,
  purchaseDate: '', warrantyExpiryDate: null, purchasePrice: 0, notes: null,
};

export default function DeviceList() {
  const [devices, setDevices] = useState<Device[]>([]);
  const [form, setForm] = useState<CreateDevice>(emptyForm);
  const [showForm, setShowForm] = useState(false);

  const load = () => deviceApi.getAll().then(setDevices);
  useEffect(() => { load(); }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await deviceApi.create(form);
    setForm(emptyForm);
    setShowForm(false);
    load();
  };

  const handleDelete = async (id: string) => {
    if (!confirm('Delete this device?')) return;
    await deviceApi.remove(id);
    load();
  };

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-2xl font-bold text-gray-800">Devices</h2>
        <button onClick={() => setShowForm(!showForm)}
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
          {showForm ? 'Cancel' : '+ Add Device'}
        </button>
      </div>
      {showForm && (
        <form onSubmit={handleSubmit} className="bg-gray-50 p-4 rounded mb-6 grid grid-cols-2 gap-4">
          <input placeholder="Name" required value={form.name}
            onChange={e => setForm({...form, name: e.target.value})} className="border p-2 rounded" />
          <input placeholder="Serial Number" required value={form.serialNumber}
            onChange={e => setForm({...form, serialNumber: e.target.value})} className="border p-2 rounded" />
          <select value={form.category} onChange={e => setForm({...form, category: +e.target.value})} className="border p-2 rounded">
            {categories.map((c, i) => <option key={i} value={i}>{c}</option>)}
          </select>
          <select value={form.status} onChange={e => setForm({...form, status: +e.target.value})} className="border p-2 rounded">
            {statuses.map((s, i) => <option key={i} value={i}>{s}</option>)}
          </select>
          <input type="date" required value={form.purchaseDate}
            onChange={e => setForm({...form, purchaseDate: e.target.value})} className="border p-2 rounded" />
          <input type="number" step="0.01" placeholder="Price" required value={form.purchasePrice || ''}
            onChange={e => setForm({...form, purchasePrice: +e.target.value})} className="border p-2 rounded" />
          <button type="submit" className="col-span-2 bg-green-600 text-white py-2 rounded hover:bg-green-700">Save Device</button>
        </form>
      )}
      <table className="w-full border-collapse">
        <thead>
          <tr className="bg-gray-100 text-left">
            <th className="p-3">Name</th><th className="p-3">Serial</th>
            <th className="p-3">Category</th><th className="p-3">Status</th>
            <th className="p-3">Price</th><th className="p-3">Actions</th>
          </tr>
        </thead>
        <tbody>
          {devices.map(d => (
            <tr key={d.id} className="border-t hover:bg-gray-50">
              <td className="p-3">{d.name}</td>
              <td className="p-3 font-mono text-sm">{d.serialNumber}</td>
              <td className="p-3">{categories[d.category]}</td>
              <td className="p-3">
                <span className={`px-2 py-1 rounded text-sm ${d.status === 0 ? 'bg-green-100 text-green-800' : d.status === 1 ? 'bg-blue-100 text-blue-800' : d.status === 2 ? 'bg-yellow-100 text-yellow-800' : 'bg-red-100 text-red-800'}`}>{statuses[d.status]}</span>
              </td>
              <td className="p-3">{d.purchasePrice.toFixed(2)} EUR</td>
              <td className="p-3">
                <button onClick={() => handleDelete(d.id)} className="text-red-600 hover:underline text-sm">Delete</button>
              </td>
            </tr>
          ))}
          {devices.length === 0 && <tr><td colSpan={6} className="p-6 text-center text-gray-400">No devices yet</td></tr>}
        </tbody>
      </table>
    </div>
  );
}
