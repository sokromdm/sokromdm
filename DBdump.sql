--
-- PostgreSQL database dump
--

-- Dumped from database version 11.5
-- Dumped by pg_dump version 11.5

-- Started on 2019-08-18 08:08:58

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 198 (class 1255 OID 24968)
-- Name: vc_insert(text, text, text, character varying, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.vc_insert(_title text, _name text, _description text, _date character varying, _salary_min integer, _salary_max integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
	insert into Vacancies(vc_title, vc_name, vc_description, vc_date, vc_salary_min, vc_salary_max)
	values(_title, _name, _description, _date, _salary_min, _salary_max);
	if found then --inserted succcessfully
		return 1;
	else return 0; --inserted fail
	end if;
end
$$;


ALTER FUNCTION public.vc_insert(_title text, _name text, _description text, _date character varying, _salary_min integer, _salary_max integer) OWNER TO postgres;

--
-- TOC entry 199 (class 1255 OID 24970)
-- Name: vc_select(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.vc_select() RETURNS TABLE(_id integer, _title text, _name text, _description text, _date character varying, _salary_min integer, _salary_max integer)
    LANGUAGE plpgsql
    AS $$
begin
	return query
	select vc_id, vc_title, vc_name, vc_description, vc_date, vc_salary_min, vc_salary_max
	from Vacancies order by vc_id;
end
$$;


ALTER FUNCTION public.vc_select() OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 197 (class 1259 OID 24580)
-- Name: vacancies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vacancies (
    vc_id integer NOT NULL,
    vc_title text,
    vc_name text,
    vc_description text,
    vc_date character varying(15),
    vc_salary_min integer,
    vc_salary_max integer
);


ALTER TABLE public.vacancies OWNER TO postgres;

--
-- TOC entry 196 (class 1259 OID 24578)
-- Name: vacancies_vc_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.vacancies_vc_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.vacancies_vc_id_seq OWNER TO postgres;

--
-- TOC entry 2817 (class 0 OID 0)
-- Dependencies: 196
-- Name: vacancies_vc_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.vacancies_vc_id_seq OWNED BY public.vacancies.vc_id;


--
-- TOC entry 2688 (class 2604 OID 24583)
-- Name: vacancies vc_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vacancies ALTER COLUMN vc_id SET DEFAULT nextval('public.vacancies_vc_id_seq'::regclass);


--
-- TOC entry 2690 (class 2606 OID 24585)
-- Name: vacancies vacancies_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vacancies
    ADD CONSTRAINT vacancies_pkey PRIMARY KEY (vc_id);


-- Completed on 2019-08-18 08:08:58

--
-- PostgreSQL database dump complete
--

