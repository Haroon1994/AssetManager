import axios from 'axios';

export interface Device {
  id: string;
  name: string;
  serialNumber: string;
  category: number;
  status: number;
  purchaseDate: string;
  warrantyExpiryDate: string | null;
  purchasePrice: number;
  notes: string | null;
}

export interface CreateDevice {
  name: string;
  serialNumber: string;
  category: number;
  status: number;
  purchaseDate: string;
  warrantyExpiryDate: string | null;
  purchasePrice: number;
  notes: string | null;
}

export interface Employee {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  department: string;
}

export interface CreateEmployee {
  firstName: string;
  lastName: string;
  email: string;
  department: string;
}

export interface Assignment {
  id: string;
  deviceId: string;
  deviceName: string;
  employeeId: string;
  employeeName: string;
  assignedAt: string;
  returnedAt: string | null;
  notes: string | null;
}

export interface CreateAssignment {
  deviceId: string;
  employeeId: string;
  notes: string | null;
}

const http = axios.create({ baseURL: '/api' });

export const deviceApi = {
  getAll: () => http.get<Device[]>('/devices').then(r => r.data),
  getById: (id: string) => http.get<Device>(`/devices/${id}`).then(r => r.data),
  create: (data: CreateDevice) => http.post<Device>('/devices', data).then(r => r.data),
  update: (id: string, data: CreateDevice) => http.put<Device>(`/devices/${id}`, data).then(r => r.data),
  remove: (id: string) => http.delete(`/devices/${id}`),
};

export const employeeApi = {
  getAll: () => http.get<Employee[]>('/employees').then(r => r.data),
  getById: (id: string) => http.get<Employee>(`/employees/${id}`).then(r => r.data),
  create: (data: CreateEmployee) => http.post<Employee>('/employees', data).then(r => r.data),
  update: (id: string, data: CreateEmployee) => http.put<Employee>(`/employees/${id}`, data).then(r => r.data),
  remove: (id: string) => http.delete(`/employees/${id}`),
};

export const assignmentApi = {
  getAll: () => http.get<Assignment[]>('/assignments').then(r => r.data),
  create: (data: CreateAssignment) => http.post<Assignment>('/assignments', data).then(r => r.data),
  returnDevice: (id: string) => http.patch<Assignment>(`/assignments/${id}/return`).then(r => r.data),
  remove: (id: string) => http.delete(`/assignments/${id}`),
};
