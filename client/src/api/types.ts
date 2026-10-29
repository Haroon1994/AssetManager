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
