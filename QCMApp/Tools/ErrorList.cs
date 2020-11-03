using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.Tools
{
    public class ErrorList
    {
        // message d'erreurs pour le controller questionnaire
        public static String generale = "Problème de traitement";
        public static String listeQuestionnaire = "Problème avec le chargement de la liste des questionnaires";
        public static String intituleQuestionnaire = "Problème avec la création de l'intitulé du questionnaire";
        public static String pageCreateQuestionnaire = "Problème pour accéder à la page création du questionnaire";
        public static String createQuestionnaireJquery = "Problème pour créé le questionnaire";
        public static String deleteQuestionnaire = "Problème pour écraser le questionnaire";
        public static String updateActifException = "Problème pour activer un questionnaire";
        public static String searchFilterQuestionnaire = "Problème pour filtrer les questionnaires";
        public static String listQuestionnaireBySite = "Problème pour charger la liste des sites";

        //messages d'erreurs pour le controller question
        public static String PageUpdateQuestion = "Problème pour acceder à la page update de la question";
        public static String createQuestion = "Problème pour lors de la création de la question";
        public static String updateQuestionJquery = "Problème pour effectuer la mise à jour de la question";
        public static String deleteQuestion = "Problème pour supprimer la question";

        //messages d'erreurs pour le controller Description
        public static String pageCreateDescription = "Problème pour afficher la page de création de description";
        public static String createDescription = "Problème pour créer la description";
        public static String deleteDescription = "Problème supprimer la description";
        public static String pageUpdateDescription = "Problème pour accéder à la page de mis à jour de la description";
        public static String UpdateDescriptionAsyn = "Problème pour effectuer mise à jour de la description";

        //messages erreurs pour le controller media
        public static String addImageUp = "Problème pour insérer une image en haut";
        public static String addImageDown = "Problème pour insérer une image en bas";
        public static String addImageChoix = "Problème pour insérer une image pour le choix";
        public static String deleteImage = "Problème pour pour supprimer une image";
        public static String deleteImageChoix = "Problème pour supprimer une image de choix";
        public static String addVideo = "Problème pour insérer une nouvelle vidéo";
        public static String deleteVideo = "Problème pour supprimer une vidéo";

        //messages erreurs pour le controller choix
        public static String deleteChoix = "Problème pour supprimer le choix";
        public static String updateChoix = "Problème sauvegarder le choix";
        public static String insertChoix = "Problème sauvegarder le choix";

        //messages erreurs pour le controller element
        public static String elementDown = "Problème faire descendre l'élément";
        public static String elementUp = "Problème faire monter l'élément";

        //messages erreurs pour le controller userQuestionnaire
        public static String IndexJqueryUserQuestionnaire  = "Problème pour commencer le questionnaire";
        public static String commencerQuestionnaire = "Problème pour commencer le questionnaire";
        public static String suiteDescription = "Problème survenu pour continuer le questionnaire";
        public static String suiteQuestion = "Problème survenu pour continuer le questionnaire";
        public static String resultatQuestionnaire = "Problème pour afficher le résultat";
        public static String retourDescription = "Problème pour revenir sur la description d'avant";
        public static String apresDescription = "Problème pour revenir sur la description d'après";
        public static String reponseQuestion = "Problème lors de la réponse à la question";
    }
}