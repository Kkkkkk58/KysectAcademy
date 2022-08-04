package dao;

import java.util.List;

public interface GenericDAO <T, K>
{
	void add(T obj);
	List<T> getAll();
	T getByID(K id);
	void update(T obj);
	void delete(T obj);
}
