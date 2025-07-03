import { createContext, useContext, useState, useCallback, useEffect } from 'react';
import { getTeachers } from '@/Services/teachers';

const TeachersContext = createContext();

export const TeachersProvider = ({ children, ...props }) => {
  const context = useCreateTeachersContext(props);
  return <TeachersContext.Provider value={context}>{children}</TeachersContext.Provider>;
};

export function useTeachersContext() {
  const context = useContext(TeachersContext);
  if (!context) throw new Error('Use app context within provider!');
  return context;
}

export const useCreateTeachersContext = function(props) {
  const [ teachers, setTeachers ] = useState([]);
  
   const fetchTeachers = useCallback(async () => {
    try {
      const data = await getTeachers();
      setTeachers(data);
    } catch (error) {
      console.error("Ошибка загрузки учителей:", error);
    }
  }, []); 

  useEffect(() => {
    fetchTeachers();
  }, [fetchTeachers]); 

  const addTeacherToState = (newTeacher) => {
    setTeachers(prev => [...prev, newTeacher]);
  };  

  const removeTeacherFromState = (teacherId) => {
    setTeachers(prev => prev.filter(teacher => teacher.id !== teacherId));
  };
  
  return {
    teachers,
    setTeachers,
    addTeacherToState,
    removeTeacherFromState
  };
}