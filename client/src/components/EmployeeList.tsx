import { useEffect, useState } from 'react';
import { employeeApi } from '../api/api';
import type { Employee, CreateEmployee } from '../api/api';

const emptyForm: CreateEmployee = {
  firstName: '', lastName: '', email: '', department: '',
};

export default function EmployeeList() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [form, setForm] = useState<CreateEmployee>(emptyForm);
  const [showForm, setShowForm] = useState(false);

  const load = () => employeeApi.getAll().then(setEmployees);
  useEffect(() => { load(); }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await employeeApi.create(form);
    setForm(emptyForm);
    setShowForm(false);
    load();
  };

  const handleDelete = async (id: string) => {
    if (!confirm('Delete this employee?')) return;
    await employeeApi.remove(id);
    load();
  };

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-2xl font-bold text-gray-800">Employees</h2>
        <button onClick={() => setShowForm(!showForm)}
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
          {showForm ? 'Cancel' : '+ Add Employee'}
        </button>
      </div>
      {showForm && (
        <form onSubmit={handleSubmit} className="bg-gray-50 p-4 rounded mb-6 grid grid-cols-2 gap-4">
          <input placeholder="First Name" required value={form.firstName}
            onChange={e => setForm({...form, firstName: e.target.value})} className="border p-2 rounded" />
          <input placeholder="Last Name" required value={form.lastName}
            onChange={e => setForm({...form, lastName: e.target.value})} className="border p-2 rounded" />
          <input type="email" placeholder="Email" required value={form.email}
            onChange={e => setForm({...form, email: e.target.value})} className="border p-2 rounded" />
          <input placeholder="Department" required value={form.department}
            onChange={e => setForm({...form, department: e.target.value})} className="border p-2 rounded" />
          <button type="submit" className="col-span-2 bg-green-600 text-white py-2 rounded hover:bg-green-700">Save Employee</button>
        </form>
      )}
      <table className="w-full border-collapse">
        <thead>
          <tr className="bg-gray-100 text-left">
            <th className="p-3">Name</th><th className="p-3">Email</th>
            <th className="p-3">Department</th><th className="p-3">Actions</th>
          </tr>
        </thead>
        <tbody>
          {employees.map(emp => (
            <tr key={emp.id} className="border-t hover:bg-gray-50">
              <td className="p-3">{emp.firstName} {emp.lastName}</td>
              <td className="p-3">{emp.email}</td>
              <td className="p-3">{emp.department}</td>
              <td className="p-3">
                <button onClick={() => handleDelete(emp.id)} className="text-red-600 hover:underline text-sm">Delete</button>
              </td>
            </tr>
          ))}
          {employees.length === 0 && <tr><td colSpan={4} className="p-6 text-center text-gray-400">No employees yet</td></tr>}
        </tbody>
      </table>
    </div>
  );
}
