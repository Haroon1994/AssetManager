import { useEffect, useState } from 'react';
import { assignmentApi, deviceApi, employeeApi } from '../api/api';
import type { Assignment, CreateAssignment, Device, Employee } from '../api/api';

export default function AssignmentList() {
  const [assignments, setAssignments] = useState<Assignment[]>([]);
  const [devices, setDevices] = useState<Device[]>([]);
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [form, setForm] = useState<CreateAssignment>({ deviceId: '', employeeId: '', notes: null });
  const [showForm, setShowForm] = useState(false);

  const load = () => {
    assignmentApi.getAll().then(setAssignments);
    deviceApi.getAll().then(setDevices);
    employeeApi.getAll().then(setEmployees);
  };
  useEffect(() => { load(); }, []);

  const availableDevices = devices.filter(d => d.status === 0);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await assignmentApi.create(form);
    setForm({ deviceId: '', employeeId: '', notes: null });
    setShowForm(false);
    load();
  };

  const handleReturn = async (id: string) => {
    await assignmentApi.returnDevice(id);
    load();
  };

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-2xl font-bold text-gray-800">Assignments</h2>
        <button onClick={() => setShowForm(!showForm)}
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
          {showForm ? 'Cancel' : '+ Assign Device'}
        </button>
      </div>
      {showForm && (
        <form onSubmit={handleSubmit} className="bg-gray-50 p-4 rounded mb-6 grid grid-cols-2 gap-4">
          <select required value={form.deviceId}
            onChange={e => setForm({...form, deviceId: e.target.value})} className="border p-2 rounded">
            <option value="">-- Select Device --</option>
            {availableDevices.map(d => <option key={d.id} value={d.id}>{d.name} ({d.serialNumber})</option>)}
          </select>
          <select required value={form.employeeId}
            onChange={e => setForm({...form, employeeId: e.target.value})} className="border p-2 rounded">
            <option value="">-- Select Employee --</option>
            {employees.map(emp => <option key={emp.id} value={emp.id}>{emp.firstName} {emp.lastName}</option>)}
          </select>
          <button type="submit" className="col-span-2 bg-green-600 text-white py-2 rounded hover:bg-green-700">Assign</button>
        </form>
      )}
      <table className="w-full border-collapse">
        <thead>
          <tr className="bg-gray-100 text-left">
            <th className="p-3">Device</th><th className="p-3">Employee</th>
            <th className="p-3">Assigned</th><th className="p-3">Status</th>
            <th className="p-3">Actions</th>
          </tr>
        </thead>
        <tbody>
          {assignments.map(a => (
            <tr key={a.id} className="border-t hover:bg-gray-50">
              <td className="p-3">{a.deviceName}</td>
              <td className="p-3">{a.employeeName}</td>
              <td className="p-3">{new Date(a.assignedAt).toLocaleDateString()}</td>
              <td className="p-3">
                {a.returnedAt
                  ? <span className="bg-gray-100 text-gray-600 px-2 py-1 rounded text-sm">Returned</span>
                  : <span className="bg-green-100 text-green-800 px-2 py-1 rounded text-sm">Active</span>}
              </td>
              <td className="p-3">
                {!a.returnedAt && <button onClick={() => handleReturn(a.id)} className="text-orange-600 hover:underline text-sm">Return</button>}
              </td>
            </tr>
          ))}
          {assignments.length === 0 && <tr><td colSpan={5} className="p-6 text-center text-gray-400">No assignments yet</td></tr>}
        </tbody>
      </table>
    </div>
  );
}
