import { useState } from 'react';
import DeviceList from './components/DeviceList';
import EmployeeList from './components/EmployeeList';
import AssignmentList from './components/AssignmentList';

type Tab = 'devices' | 'employees' | 'assignments';

export default function App() {
  const [tab, setTab] = useState<Tab>('devices');

  return (
    <div className="min-h-screen bg-gray-100">
      <header className="bg-white shadow">
        <div className="max-w-6xl mx-auto px-4 py-4">
          <h1 className="text-3xl font-bold text-gray-900">IT Asset Manager</h1>
          <p className="text-gray-500 text-sm mt-1">Track devices, employees, and assignments</p>
        </div>
      </header>

      <nav className="bg-white border-b">
        <div className="max-w-6xl mx-auto px-4 flex gap-1">
          {(['devices', 'employees', 'assignments'] as Tab[]).map(t => (
            <button key={t} onClick={() => setTab(t)}
              className={`px-4 py-3 text-sm font-medium capitalize ${
                tab === t
                  ? 'border-b-2 border-blue-600 text-blue-600'
                  : 'text-gray-500 hover:text-gray-700'
              }`}>
              {t}
            </button>
          ))}
        </div>
      </nav>

      <main className="max-w-6xl mx-auto px-4 py-8">
        <div className="bg-white rounded-lg shadow p-6">
          {tab === 'devices' && <DeviceList />}
          {tab === 'employees' && <EmployeeList />}
          {tab === 'assignments' && <AssignmentList />}
        </div>
      </main>
    </div>
  );
}
